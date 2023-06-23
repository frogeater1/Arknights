using System;
using System.Collections;
using System.Collections.Generic;
using Arknights.Pools;
using UnityEngine;

namespace Arknights
{
    public class PoolManager : MonoBehaviour
{

    public AttackRange attackRange;

    public HpSpSliders hpSpSliders;
        
    public DamageValues damageValues;

    public SkillEffects skillEffects;

    public Pools.Buffs buffs; 

    

    public void Init()
    {
        attackRange.CreatePool();
        hpSpSliders.CreatePool();
        damageValues.CreatePool();
        skillEffects.CreatePool();
        buffs.CreatePool();
    }
}
}