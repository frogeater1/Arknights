using System;
using System.Collections.Generic;
using Arknights.Pools;
using UnityEngine;
using UnityEngine.Pool;

namespace Arknights
{
    public class AttackRange : PoolParent
    {
        private List<GameObject> attackGrids;

        private void OnEnable()
        {
            EventManager.ChangeDirection += OnChangeDirection;
            EventManager.CancelSelect += Hide;
        }

        private void OnDisable()
        {
            EventManager.ChangeDirection -= OnChangeDirection;
            EventManager.CancelSelect -= Hide;
        }

        private void OnChangeDirection(Character character, 方向 direction)
        {
            switch (direction)
            {
                case 方向.取消:
                    Hide();
                    break;
                case 方向.右:
                case 方向.下:
                case 方向.左:
                case 方向.上:
                    Show(character, direction);
                    break;
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
            // Game.Instance.PoolManager.pools.Add(this);
        }

        public void Show(Character character, 方向 dir = 方向.右)
        {
            // transform.localRotation = Quaternion.Euler(0, rotation, 0);
            transform.localPosition = new Vector3(character.logicPos.x + 0.5f, 0.03f, character.logicPos.y + 0.5f);

            if (attackGrids != null)
            {
                foreach (var g in attackGrids)
                {
                    pool.Release(g);
                }
            }

            attackGrids = new List<GameObject>();
            foreach (var range in character.attackRange)
            {
                var attack_grid = pool.Get();

                var localPos = Map.Instance.CalculPos(Vector2Int.zero, dir, range);
                attack_grid.transform.localPosition = new Vector3(localPos.x, 0, localPos.y);
                var position = attack_grid.transform.position;
                var grid_type = Map.Instance.GetGrid(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z))?.type;
                if (grid_type == GridType.不站人高台 || grid_type == GridType.站人高台)
                {
                    var localPosition = attack_grid.transform.localPosition;
                    attack_grid.transform.localPosition = new Vector3(localPosition.x, 0.53f, localPosition.z);
                }

                attackGrids.Add(attack_grid);
            }
        }


        public void Hide()
        {
            if (attackGrids == null)
            {
                return;
            }

            foreach (var g in attackGrids)
            {
                pool.Release(g);
            }

            attackGrids = null;
        }
    }
}