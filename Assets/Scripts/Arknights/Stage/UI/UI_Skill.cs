using Arknights.Skills;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Arknights
{
    public partial class UI_Skill
    {
        public 主动 skill;
        public void Show(Skill s)
        {
            skill = (主动)s;
            m_skill_button.icon = skill.iconURL;
            Refresh();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            Refresh();
        }

        private void Refresh()
        {
            if (skill)
            {
                var cost_E = skill.loadData.cost_e[skill.level - 1];
                if (skill.curE >= cost_E)
                {
                    m_ready.selectedPage = "ready";
                }
                else
                {
                    m_ready.selectedPage = "loading";
                    m_electricity.text = skill.curE + "/" + cost_E;
                }
            }
        }
    }
}