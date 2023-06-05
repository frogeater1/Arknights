using System;
using System.Collections.Generic;
using Arknights.Pools;
using UnityEngine;
using UnityEngine.Pool;

namespace Arknights
{
    public class AttackRange : PoolParent
    {
        public PoolType pooltype = PoolType.攻击范围;

        private List<GameObject> grids;

        private void OnEnable()
        {
            EventManager.ChangeDirection += OnChangeDirection;
        }

        private void OnDisable()
        {
            EventManager.ChangeDirection -= OnChangeDirection;
        }

        private void OnChangeDirection(方向 direction)
        {
            switch (direction)
            {
                case 方向.取消: Hide(); break;
                case 方向.右: Show(0); break;
                case 方向.下: Show(90); break;
                case 方向.左: Show(180); break;
                case 方向.上: Show(270); break;
            }
        }


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

        public void Show(int rotation = 0)
        {
            Character curCharacter = Game.Instance.curCharacter;
            transform.localRotation = Quaternion.Euler(0, rotation, 0);
            transform.localPosition = new Vector3(curCharacter.transform.localPosition.x, 0.03f, Mathf.Floor(curCharacter.transform.localPosition.z) + 0.5f);
    
            if (grids != null)
            {
                foreach (var grid in grids)
                {
                    pool.Release(grid);
                }
            }
            grids = new List<GameObject>();
            foreach (var range in curCharacter.attackRange)
            {
                var grid = pool.Get();
                grid.transform.localPosition = new Vector3(range.x, 0, range.y);
                grids.Add(grid);
            }
        }


        public void Hide()
        {
            if (grids == null)
            {
                return;
            }

            foreach (var grid in grids)
            {
                pool.Release(grid);
            }

            grids = null;
        }
    }
}