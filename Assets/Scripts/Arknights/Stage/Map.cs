﻿using System;
using System.Collections;
using System.Collections.Generic;
using Arknights;
using Arknights.Pools;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class Map : Singleton<Map>, ILoadable
{
    public GameObject decal_地面;
    public GameObject decal_高台;
    
    public AttackRange attackRange;
    

    [SerializeField]
    private GridType[] gridData;

    [SerializeField]
    private int height;

    [SerializeField]
    public int width;

#if UNITY_EDITOR
    public void Load(List<GridType> mapDataList, int w, int h)
    {
        gridData = mapDataList.ToArray();
        width = w;
        height = h;
    }
#endif
    
    public GridType GetGridType(int x, int z)
    {
        //example: (1,6)=> (height - z - 1,x)=>(2,1) => 19
        return gridData[(height - z - 1) * width + x];
    }
}