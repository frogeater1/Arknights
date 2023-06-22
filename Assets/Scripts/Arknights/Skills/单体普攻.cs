namespace Arknights.Skills
{
    public class 单体普攻 : 主动
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