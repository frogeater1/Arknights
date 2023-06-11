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
                    Show(character, 0);
                    break;
                case 方向.下:
                    Show(character, 90);
                    break;
                case 方向.左:
                    Show(character, 180);
                    break;
                case 方向.上:
                    Show(character, 270);
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

        public void Show(Character character, int rotation = 0)
        {
            transform.localRotation = Quaternion.Euler(0, rotation, 0);
            transform.localPosition = new Vector3(character.transform.localPosition.x, 0.03f, Mathf.Floor(character.transform.localPosition.z) + 0.5f);

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

                attack_grid.transform.localPosition = new Vector3(range.x, 0, range.y);
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