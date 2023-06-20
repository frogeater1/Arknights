//这个类用来存放跨场景共享的数据

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arknights
{
    public static class Parking
    {
        public static List<Character>[] prefabs = new List<Character>[2];

        public static Room room;
        public static int meId;

        public static void StartBattle(Player p1, Player p2, int me)
        {
            room = new Room
            {
                players = new[] { p1, p2 },
            };

            meId = me;
            prefabs[0] = new List<Character>();
            foreach (int id in p1.selectCardIdxs)
            {
                var prefab = Main.Instance.characterPrefabs[id];
                prefabs[0].Add(prefab);
            }

            prefabs[1] = new List<Character>();
            foreach (int id in p2.selectCardIdxs)
            {
                var prefab = Main.Instance.characterPrefabs[id];
                prefabs[1].Add(prefab);
            }

            LoadScene("Stage").Forget();
        }

        public static async UniTaskVoid LoadScene(string scene)
        {
            var loading = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
            loading.allowSceneActivation = false;
            Main.Instance.ui_loading.FadeOut();
            await UniTask.WaitUntil(() => Main.Instance.ui_loading.m_stage.visible);
            await UniTask.Delay(500);
            loading.allowSceneActivation = true;
        }
    }
}