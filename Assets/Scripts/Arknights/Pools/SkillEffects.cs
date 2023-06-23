using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace Arknights.Pools
{
    public class SkillEffects : MonoBehaviour
    {
        public GameObject[] prefabs;
        public Dictionary<string, ObjectPool<GameObject>> pools = new();
        public  void CreatePool()
        {
            for (int i = 0; i < prefabs.Length; i++)
            {
                var prefab = prefabs[i];
                pools.Add(prefab.name, new ObjectPool<GameObject>(
                    () => Instantiate(prefab, transform),
                    go => go.SetActive(true),
                    go => go.SetActive(false),
                    Destroy
                ));
            }
        }

        public async UniTaskVoid ShowEffect(string effectName, Vector2 logicPos, int duration)
        {
            var go = pools[effectName].Get();
            go.transform.position = new Vector3(logicPos.x + 0.5f, 0, logicPos.y + 0.5f);
            await UniTask.Delay(duration);
            pools[effectName].Release(go);
        }
    }
}