using System;
using System.Collections.Generic;
using Arknights.Data;
using FairyGUI;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Arknights
{
    public class CharacterManager : MonoBehaviour
    {
        public Character[] characters;
        public Character[] enemies;

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
            characters = new Character[Parking.characterPrefabs.Count];
            for (int i = 0; i < Parking.characterPrefabs.Count; i++)
            {
                var go = Instantiate(Parking.characterPrefabs[i], new Vector3(1000, 0, 0),
                    Quaternion.Euler(Vector3.zero), Game.Instance.transform);
                characters[i] = go;
                go.name = "me_" + i;
                //todo： 永久升级，强化，皮肤，技能穿戴之类的属性在这读Parking存的东西计算上去,暂时先直接写死
                go.Init(Parking.room.me);
            }

            enemies = new Character[Parking.enemiesPrefabs.Count];
            for (int i = 0; i < Parking.enemiesPrefabs.Count; i++)
            {
                var go = Instantiate(Parking.enemiesPrefabs[i], new Vector3(1000, 0, 0),
                    Quaternion.Euler(Vector3.zero), Game.Instance.transform);
                characters[i] = go;
                go.name = "enemy_" + i;
                //todo: 同上
                go.Init(Parking.room.enemy);
            }
        }
    }
}