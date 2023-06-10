using FairyGUI;
using UnityEngine;

namespace Arknights.Skills
{
    public abstract class Skill : MonoBehaviour
    {
        public Data.Skill loadData;
        public string iconURL;
        public abstract void Use();

#if  UNITY_EDITOR
        
        public virtual void Load(Data.Skill skill)
        {
            iconURL = UIPackage.GetItemURL("Arknights", skill.icon_name);
            loadData = skill;
        }
#endif

        public abstract void Init();
    }
}