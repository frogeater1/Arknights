using System;
using System.Collections.Generic;
using System.Text;
using Arknights.Data;
using FairyGUI;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Arknights
{
    public class CharacterManager : MonoBehaviour
    {
        public List<Character>[] characters;

        //设置为属性可以展示usages
        private Character _curCharacter;

        public Character curCharacter
        {
            get => _curCharacter;
            set
            {
                _curCharacter = value;
                if (value)
                {
                    Game.Instance.ui_battle.ShowStats(true, value);
                    Game.Instance.CameraManager.DoRotation(new Vector3(60, 0, -3));
                }
                else
                {
                    Game.Instance.ui_battle.ShowStats(false);
                    Game.Instance.CameraManager.DoRotation(new Vector3(60, 0, 0));
                }
            }
        }

        public void Init()
        {
            characters = new List<Character>[2];
            Debug.Log("init character manager");

            for (int i = 0; i < Parking.prefabs.Length; i++)
            {
                characters[i] = new List<Character>();
                foreach (Character prefab in Parking.prefabs[i])
                {
                    var go = Instantiate(prefab, new Vector3(1000, 0, 0),
                        Quaternion.Euler(Vector3.zero), Game.Instance.transform);
                    characters[i].Add(go);
                    go.name = "player" + (i + 1) + "_" + go.name;
                    //todo： 永久升级，强化，皮肤，技能穿戴之类的属性在这读Parking存的东西计算上去,暂时先直接写死
                    go.Init(Parking.room.players[i]);
                }
            }
        }
    }
}