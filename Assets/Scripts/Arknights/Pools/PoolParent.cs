using UnityEngine;
using UnityEngine.Pool;

namespace Arknights.Pools
{
    public abstract class PoolParent : MonoBehaviour
    {
        public ObjectPool<GameObject> pool;
        public abstract void CreatePool(GameObject prefab);
    }
}