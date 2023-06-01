using System;
using System.Collections.Generic;
using Arknights.Data;
using FairyGUI;
using UnityEngine;

namespace Arknights
{
    public class CharacterManager : MonoBehaviour
    {
        //tmp
        public Character[] c;

        public Character[] characters;

        public void Init()
        {
            
            //tmp
            Parking.characterPrefabs = c;

            characters = new Character[Parking.characterPrefabs.Length];

            for (int i = 0; i < Parking.characterPrefabs.Length; i++)
            {
                var go = Instantiate(Parking.characterPrefabs[i], new Vector3(1000, 0, 0), Quaternion.Euler(Vector3.zero), Game.Instance.transform);
                characters[i] = go;
                go.transform.name = i.ToString();
                //todo： 永久升级，强化之类的属性在这算上去
            }
        }
    }
}