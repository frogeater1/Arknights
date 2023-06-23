using System.Collections.Generic;
using Arknights.Buffs;
using UnityEngine;
using UnityEngine.Pool;

namespace Arknights.Pools
{
    public class Buffs : MonoBehaviour
    {
        public Buff[] prefabs;

        public Dictionary<string, ObjectPool<Buff>> pools = new();

        public void CreatePool()
        {
            for (int i = 0; i < prefabs.Length; i++)
            {
                var prefab = prefabs[i];
                pools.Add(prefab.name, new ObjectPool<Buff>(
                    () => Instantiate(prefab, transform),
                    buff => buff.gameObject.SetActive(true),
                    buff =>
                    {
                        buff.gameObject.SetActive(false);
                        buff.unit = null;
                    },
                    Destroy
                ));
            }
        }
    }
}