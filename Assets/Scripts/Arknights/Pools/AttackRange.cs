using System;
using System.Collections.Generic;
using Arknights.Pools;
using UnityEngine;
using UnityEngine.Pool;

namespace Arknights
{
    public class AttackRange : MonoBehaviour
    {
        public GameObject[] prefabs;
        public Dictionary<string, ObjectPool<GameObject>> pools = new();
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

        private void OnChangeDirection(方向 direction)
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
                    Show(direction);
                    break;
            }
        }


        public  void CreatePool()
        {
            pools.Add(prefabs[0].name, new ObjectPool<GameObject>(
                () => Instantiate(prefabs[0], transform),
                go => go.SetActive(true),
                go => go.SetActive(false),
                Destroy
            ));
        }

        public void Show(方向 dir = 方向.右)
        {
            Character character = Game.Instance.CharacterManager.curCharacter;
            transform.localPosition = new Vector3(character.logicPos.x + 0.5f, 0.03f, character.logicPos.y + 0.5f);

            if (attackGrids != null)
            {
                foreach (var g in attackGrids)
                {
                    pools["AttackGrid"].Release(g);
                }
            }

            attackGrids = new List<GameObject>();
            foreach (var range in character.attackRange)
            {
                var attack_grid = pools["AttackGrid"].Get();

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
                pools["AttackGrid"].Release(g);
            }

            attackGrids = null;
        }
    }
}