using System;
using System.Collections.Generic;
using Arknights;
using UnityEngine;
using Grid = Arknights.Grid;

public class Map : Singleton<Map>, ILoadable
{
    public GameObject decal_地面;
    public GameObject decal_高台;

    [SerializeField]
    private GridType[] gridData;

    [SerializeField]
    private int height;

    [SerializeField]
    public int width;

    public Dictionary<Vector2Int, Grid> grids = new();

#if UNITY_EDITOR
    public void Load(List<GridType> mapDataList, int w, int h)
    {
        gridData = mapDataList.ToArray();
        width = w;
        height = h;
    }
#endif

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < gridData.Length; i++)
        {
            //29 => 29/w => (2,1) => (1,h-2-1)  
            Vector2Int logicPos = new Vector2Int(i % width, height - i / width - 1);
            grids.Add(logicPos, new Grid(gridData[i], logicPos));
        }
    }

    public Grid GetGrid(int x, int z)
    {
        grids.TryGetValue(new Vector2Int(x, z), out var grid);
        return grid;
    }

    public Grid GetGrid(Vector2Int logicPos)
    {
        grids.TryGetValue(logicPos, out var grid);
        return grid;
    }

    //检查这个格子上有没有指定角色
    public Unit CheckGrid(Vector2Int logicPos, Team team)
    {
        if (grids.TryGetValue(logicPos, out var grid))
        {
            if (grid.units != null)
            {
                foreach (var u in grid.units)
                {
                    if (u.player.team == team)
                    {
                        return u;
                    }
                }
            }
        }

        return null;
    }

    public void AddUnit(Character character)
    {
        if (grids.TryGetValue(character.logicPos, out var grid))
        {
            if (grid.units == null)
            {
                grid.units = new List<Unit>();
            }

            grid.units.Add(character);
        }
    }

    public void RemoveUnit(Character character)
    {
        if (grids.TryGetValue(character.logicPos, out var grid))
        {
            if (grid.units != null)
            {
                grid.units.Remove(character);
            }
        }
    }

    public Vector2Int CalculPos(Vector2Int logicPos, 方向 attackDir, Vector2Int range)
    {
        return attackDir switch
        {
            方向.右 => logicPos + range,
            方向.下 => new Vector2Int(logicPos.x + range.y, logicPos.y - range.x),
            方向.左 => new Vector2Int(logicPos.x - range.x, logicPos.y - range.y),
            方向.上 => new Vector2Int(logicPos.x - range.y, logicPos.y + range.x),
            _ => throw new ArgumentOutOfRangeException(nameof(attackDir), attackDir, null)
        };
    }
}