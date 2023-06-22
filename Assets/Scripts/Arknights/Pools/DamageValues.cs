using Arknights.UGUI;
using UnityEngine;
using UnityEngine.Pool;

namespace Arknights.Pools
{
    public class DamageValues : PoolParent
    {
        public override void CreatePool(GameObject prefab)
        {
            pool = new ObjectPool<GameObject>(
                () => Instantiate(prefab, transform),
                go => go.SetActive(true),
                go => go.SetActive(false),
                Destroy
            );
        }
        
        
        public void ShowDamageValue(Unit unit, int damage)
        {
            var go = pool.Get();
            
        }
        
        public void HideDamageValue(Unit unit, DamageValue damageValue)
        {
       
        }

    }
}