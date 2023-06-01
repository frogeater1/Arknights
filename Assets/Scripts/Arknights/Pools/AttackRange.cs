using FairyGUI;
using UnityEngine;
using UnityEngine.Pool;

namespace Arknights.Pools
{
    public class AttackRange : PoolParent
    {
        public PoolType pooltype = PoolType.攻击范围;

        public override void CreatePool(GameObject prefab)
        {
            pool = new ObjectPool<GameObject>(
                () => Instantiate(prefab, transform),
                go => go.SetActive(true),
                go => go.SetActive(false),
                Destroy
            );
            Game.Instance.PoolManager.pools.Add(this);
        }
    }
}