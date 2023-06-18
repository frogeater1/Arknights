using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FairyGUI;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Arknights.Data
{
    public static partial class Database
    {
        [MenuItem("Tools/ExportExcel")]
        public static void ExportExcel()
        {
#if UNITY_EDITOR_OSX
            var path = Environment.GetEnvironmentVariable("PATH")!;
            if (!path.Contains("/usr/local/bin"))
            {
                Environment.SetEnvironmentVariable("PATH", path + ":/usr/local/bin");
            }
#endif

            var process = Process.Start(new ProcessStartInfo("node", "excel-exporter/dist/main.js")
            {
                WorkingDirectory = "..",
                UseShellExecute = false,
                Environment =
                {
                    ["excel"] = "Config",
                    ["json"] = "Arknights/Assets/Import/Data",
                    ["cs"] = "Arknights/Assets/Import/DataScripts",
                    ["namespace"] = "Arknights.Data",
                    ["database"] = "true"
                }
            })!;
            process.WaitForExit();
            process.Close();
            AssetDatabase.Refresh();


            try
            {
                AssetDatabase.StartAssetEditing();

                //json转成c#数组
                LoadAll();

                MakeCharacter();

                MakeMap();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        [MenuItem("Tools/Proto2cs")]
        public static void Proto2cs()
        {

            var process = Process.Start(new ProcessStartInfo()
            {
                UseShellExecute = true,
                WorkingDirectory = "..",
                FileName = "build.bat",
            })!;
            process.WaitForExit();
            process.Close();

            Debug.Log("proto2cs success!");
        }


        public static T Load<T>(string name)
        {
            var asset = LoadAsset<TextAsset>($"Assets/Import/Data/{name}.json");
            return JsonConvert.DeserializeObject<T>(asset.text);
        }

        public static T LoadAsset<T>(string path) where T : Object
        {
            var result = AssetDatabase.LoadAssetAtPath<T>(path);
            Assert.IsNotNull(result, path);
            return result;
        }

        private static void MakeCharacter()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Arknights.Character>("Assets/Prefabs/Battle/character.prefab");
            foreach (var c in Character)
            {
                var go = PrefabUtility.InstantiatePrefab(prefab) as Arknights.Character;
                try
                {
                    go.Load(c);
                    PrefabUtility.SaveAsPrefabAsset(go.gameObject, $"Assets/Import/Characters/char_{c.id}.prefab");
                }
                finally
                {
                    Object.DestroyImmediate(go.gameObject);
                }
            }
        }


        private static void MakeMap()
        {
            //todo:多关卡读取，这里先只读第0关

            int width = Grid[0].gridline.Length;
            int height = Grid.Length;

            var texture_地面 = new Texture2D(width, height);
            var texture_高台 = new Texture2D(width, height);

            var map_data_list = new List<GridType>();

            //example: (3,8)=>(8,5)=>(j,height-i-1)

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map_data_list.Add(Grid[i].gridline[j]);
                    if (Grid[i].gridline[j] != GridType.站人地面 && Grid[i].gridline[j] != GridType.站人高台)
                    {
                        texture_地面.SetPixel(j, height - i - 1, new Color(0, 0, 0, 0));
                        texture_高台.SetPixel(j, height - i - 1, new Color(0, 0, 0, 0));
                    }
                    else if (Grid[i].gridline[j] == GridType.站人高台)
                    {
                        texture_地面.SetPixel(j, height - i - 1, new Color(0, 0, 0, 0));
                        texture_高台.SetPixel(j, height - i - 1, Color.green);
                    }
                    else
                    {
                        texture_地面.SetPixel(j, height - i - 1, Color.green);
                        texture_高台.SetPixel(j, height - i - 1, new Color(0, 0, 0, 0));
                    }
                }
            }

            //必须在换场景之前保存否则会丢失，报错：Passed in texture is invalid.
            System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/Textures/texture_地面.png",
                texture_地面.EncodeToPNG());
            System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/Textures/texture_高台.png",
                texture_高台.EncodeToPNG());

            // var cur_scene_path = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().path;
            var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/Stage.unity");
            foreach (var obj in Object.FindObjectsOfType<MonoBehaviour>().OfType<ILoadable>())
            {
                obj.Load(map_data_list, width, height);
            }

            //保存并打开之前的场景
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
            UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
            // UnityEditor.SceneManagement.EditorSceneManager.OpenScene(cur_scene_path);
        }
    }
}