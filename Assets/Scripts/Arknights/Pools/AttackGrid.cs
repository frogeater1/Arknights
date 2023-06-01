using System;
using UnityEngine;

namespace Arknights.Pools
{
    public class AttackGrid : MonoBehaviour
    {
        private void Update()
        {
            var grid_type = Map.Instance.GetGridType(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z));
            if (grid_type == GridType.不站人高台 || grid_type == GridType.站人高台)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, 0.6f, transform.localPosition.z);
            }
        }
    }
}