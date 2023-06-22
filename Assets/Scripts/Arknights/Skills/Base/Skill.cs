using FairyGUI;
using UnityEngine;

namespace Arknights.Skills
{
    public abstract class Skill : MonoBehaviour
    {
        public Data.Skill loadData;
        public string iconURL;

        public int level;

        public bool carrying;

        public abstract void Use(Unit self, Unit target = null);

#if UNITY_EDITOR

        public virtual void Load(Data.Skill skill)
        {
            iconURL = UIPackage.GetItemURL("Arknights", skill.icon_name);
            loadData = skill;
        }
#endif
    }
}