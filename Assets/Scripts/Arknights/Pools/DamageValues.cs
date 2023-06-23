using System.Collections.Generic;
using Arknights.UGUI;
using UnityEngine;
using UnityEngine.Pool;

namespace Arknights.Pools
{
    public class DamageValues : MonoBehaviour
    {
        
        public GameObject[] prefabs;
        public Dictionary<string, ObjectPool<GameObject>> pools = new();
        public void CreatePool()
        {
            pools.Add(prefabs[0].name, new ObjectPool<GameObject>(
                () => Instantiate(prefabs[0], transform),
                go => go.SetActive(true),
                go => go.SetActive(false),
                Destroy
            ));
        }


        public void ShowDamageValue(Unit unit, int damage)
        {
        }

        public void HideDamageValue(Unit unit, DamageValue damageValue)
        {
        }
    }
}