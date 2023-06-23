using Arknights.Data;
using UnityEngine;

namespace Arknights.Skills
{
    public class 群体伤害 : 主动
    {
        public override void Use(Unit self, Unit target = null)
        {
            if (loadData.range_type == SkillRangeType.索引)
            {
                foreach (Vector2Int range in range)
                {
                    Vector2Int logicGrid = Map.Instance.CalculPos(self.logicPos, self.attackDir, range);
                    target = Map.Instance.CheckGrid(logicGrid, self.player.team);
                    if (target)
                    {
                        target.curHp -= Helper.CalculateValue(self.attack, loadData.ext_value_type_1, loadData.ext_values_1[level - 1]);

                        if (loadData.buff_id != null)
                        {
                            target.AddBuff(loadData.buff_id, level);
                        }
                    }
                }
            }
        }
    }
}