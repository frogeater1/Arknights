using System;
using UnityEngine;

namespace Arknights.Skills
{
    public class 主动 : Skill
    {
        public override void Use(Unit self, Unit target = null)
        {
            if (target && loadData.range_type == SkillRangeType.攻击)
            {
                target.curHp -= self.attack;
            }
        }
    }
}