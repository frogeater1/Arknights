using System;
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

    public GameObject sceneUIroot;
    
    private AttackRange attackRange;
    private List<GameObject> grids;

    public Character curCharacter
    {
        get => _curCharacter;
        set
        {
            if (value != null)
            {
                _curCharacter = value;
            }
            else
            {
                //只有在没选择任何的时候才可以赋值null;
                if (Game.Instance.ui_battle.m_card_list.selectedIndex == -1)
                {
                    _curCharacter = null;
                }
            }
        }
    }

    private Character _curCharacter;
    

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

    private void Start()
    {
        attackRange = (AttackRange)Game.Instance.PoolManager.GetPool(PoolType.攻击范围);
    }

    public GridType GetGridType(int x, int z)
    {
        //example: (1,6)=> (height - z - 1,x)=>(2,1) => 19
        return gridData[(height - z - 1) * width + x];
    }


    public void ShowAttackRange(int rotation = 0)
    {
        attackRange.transform.localRotation = Quaternion.Euler(0, rotation, 0);
        attackRange.transform.localPosition = new Vector3(curCharacter.transform.localPosition.x, 0.03f, Mathf.Floor(curCharacter.transform.localPosition.z) + 0.5f);
        if (grids == null)
        {
            grids = new List<GameObject>();
            foreach (var range in curCharacter.attackRange)
            {
                var grid = attackRange.pool.Get();
                grid.transform.localPosition = new Vector3(range.x, 0, range.y);
                grids.Add(grid);
            }
        }
        else
        {
            for (int i = 0; i < grids.Count; i++)
            {
                var range = curCharacter.attackRange[i];
                var grid = grids[i];
                grid.transform.localPosition = new Vector3(range.x, 0, range.y);
            }
        }
    }


    public void HideAttackRange()
    {
        if (grids == null)
        {
            return;
        }

        foreach (var grid in grids)
        {
            attackRange.pool.Release(grid);
        }

        grids = null;
    }

    public void SetSceneUIPos()
    {
        sceneUIroot.transform.position = new Vector3(curCharacter.transform.localPosition.x, 1f, Mathf.Floor(curCharacter.transform.localPosition.z) + 0.5f);
    }
}