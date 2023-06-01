#if UNITY_EDITOR
namespace Arknights.Data
{
    public static partial class Database
    {
        public static Character[] Character;
        public static Skill[] Skill;
        public static SkillRange[] SkillRange;
        public static Grid[] Grid;

        public static void LoadAll()
        {
            Character = Load<Character[]>("Character");
            Skill = Load<Skill[]>("Skill");
            SkillRange = Load<SkillRange[]>("SkillRange");
            Grid = Load<Grid[]>("Grid");
        }
    }
}
#endif
