﻿using Arknights.Skills;
using FairyGUI;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Arknights
{
    public partial class UI_Skill
    {
        public 主动 skill;
        public Character character;


        partial void Init()
        {
            onClick.Set(OnClick);
        }

        private void OnClick(EventContext context)
        {
            if (!skill) return;
            var cost_sp = skill.loadData.cost_e[skill.level - 1];
            if (character.curSp >= cost_sp)
            {
                character.Skill(skill);
            }
        }


        public void Show(Character character)
        {
            this.character = character;
            skill = (主动)character.skills[character.carryingSkillIdx];
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
                var cost_Sp = skill.loadData.cost_e[skill.level - 1];
                if (character.curSp >= cost_Sp)
                {
                    m_ready.selectedPage = "ready";
                }
                else
                {
                    m_ready.selectedPage = "loading";
                    m_electricity.text = character.curSp + "/" + cost_Sp;
                }
            }
        }
    }
}