//这个类用来存放跨场景共享的数据

using System.Collections.Generic;
using Arknights.Loading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arknights
{
    public static class Parking
    {
        public static Character[] characterPrefabs; //进入战斗时的队伍成员prefab

        public static void StartBattle(Character[] prefabs)
        {
            LoadScene("Stage").Forget();
            characterPrefabs = prefabs;
        }
        
        public static async UniTaskVoid LoadScene(string scene)
        {
            var loading = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
            loading.allowSceneActivation = false;
            var UI_loading = Object.FindObjectOfType<UILoading>();
            UI_loading.Fade();
            await UniTask.WaitUntil(()=>UI_loading.stage.visible);
            await UniTask.Delay(500);
            loading.allowSceneActivation = true;
        }
    }
}