﻿using System.Collections.Generic;
using Arknights.Buffs;
using Arknights.UGUI;
using Spine.Unity;
using UnityEngine;

namespace Arknights
{
    public class Unit : MonoBehaviour
    {
        public Player player;
        public Vector2Int logicPos;
        public SkeletonAnimation skeletonAnimation;
        public FSMSystem fsmSystem;
        public 方向 attackDir = 方向.右; //这个是角色攻击朝向，用来计算攻击范围的，跟上面那个没关系,高台单位在下场时即固定，地面单位为实际移动的方向，注意跟动画方向无关。
        public List<Vector2Int> attackRange;


        public 部署类型 当前部署类型;
        public HpSlider hpSlider;
        public SpSlider spSlider;

        public float attack;
        public float curHp;
        public float maxHp;
        public List<Buff> buffs;

        public void AddBuff(string buffID, int level)
        {
            var buff = Game.Instance.PoolManager.buffs.pools[buffID].Get();
            buffs.Add(buff);
            buff.Action(this, level);
        }
    }
}