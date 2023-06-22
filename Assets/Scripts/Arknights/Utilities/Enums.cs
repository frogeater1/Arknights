public enum SkillAutoTiming //技能自动释放的时机
{
    None, //不自动释放
    普攻, //角色准备就绪且能搜寻到攻击目标时
    开场, //战斗开场时,即不需要下场
    下场,
    充能完毕
}

public enum SkillRangeType //范围类型
{
    None, //不需要判断范围
    自己,
    全场,
    攻击, //范围判断由角色攻击范围决定
    索引, //范围判断由skillRangeId指定的范围判断
    特殊, //此范围类型范围判断直接写在技能逻辑的代码里
}

public enum GridType
{
    红方,
    蓝方,
    站人地面,
    不站人地面,
    站人高台,
    不站人高台,
    深坑,
    配景,
}

public enum 部署类型
{
    Both,
    地面,
    高台
}

public enum 职业
{
    先锋,
    狙击,
    法术,
    医疗,
    重装,
    近卫,
    特种,
    辅助,
}

public enum 方向
{
    取消,
    右,
    下,
    左,
    上,
    操作,
}

public enum CharacterState
{
    场下,
    场上,
}

public enum Team
{
    Blue,
    Red,
    Neutral,
}

public enum RoomState
{
    start,
    create_waiting,
    host_waiting,
    guest_waiting,
}

public enum EFSMState
{
    Idle,
    Start,
    Attack,
    Move,
    Skill,
    Die,
    Card,//这个是指在手牌状态，没有对应的动画
}