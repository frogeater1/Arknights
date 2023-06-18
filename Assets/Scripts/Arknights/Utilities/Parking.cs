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
        public static List<Character> characterPrefabs = new (); //进入战斗时的队伍成员prefab
        public static List<Character> enemiesPrefabs = new (); //进入战斗时的队伍成员prefab

        public static Room room;

        public static void StartBattle(Player player1, Player player2)
        {
            room = new Room
            {
                me = player1,
                enemy = player2,
            };
            foreach (int id in player1.selectCardIdxs)
            { 
                var prefab = Main.Instance.characterPrefabs[id];
                characterPrefabs.Add(prefab);
            }

            foreach (int id in player2.selectCardIdxs)
            {
                enemiesPrefabs.Add(Main.Instance.characterPrefabs[id]);
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