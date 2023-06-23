using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using UnityEngine;

namespace Arknights.Skills
{
    public abstract class Skill : MonoBehaviour
    {
        public Data.Skill loadData;
        public string iconURL;

        public GameObject effect;

        public int level;

        public bool carrying;

        public List<Vector2Int> range;

        public abstract void Use(Unit self, Unit target = null);

#if UNITY_EDITOR

        public virtual void Load(Data.Skill data)
        {
            iconURL = UIPackage.GetItemURL("Arknights", data.icon_name);
            loadData = data;

            if (data.range_type == SkillRangeType.索引)
            {
                Data.SkillRange skillRange = Data.Database.SkillRange.First(x => x.id == loadData.range_id);
                range = skillRange.Range;
            }
        }
#endif
    }
}