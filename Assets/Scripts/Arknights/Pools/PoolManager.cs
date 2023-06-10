using System;
using System.Collections;
using System.Collections.Generic;
using Arknights.Pools;
using UnityEngine;

namespace Arknights
{
    public class PoolManager : MonoBehaviour
{
    //注意parents和prefabs要一一对应
    public PoolParent[] poolParents;
    public GameObject[] prefabs;
    

    public void Init()
    {
        for (int i = 0; i < poolParents.Length; i++)
        {
            poolParents[i].CreatePool(prefabs[i]);
        }
    }
}
}