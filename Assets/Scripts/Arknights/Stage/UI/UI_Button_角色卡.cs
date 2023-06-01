using FairyGUI;
using UnityEngine;

namespace Arknights
{
    public partial class UI_Button_角色卡
    {
        public int idx;
        public Character character;

        public override bool selected
        {
            get { return base.selected; }
            set
            {
                base.selected = value;
                Map.Instance.decal_地面.SetActive(value && (character.loadData.部署类型 == 部署类型.地面 || character.loadData.部署类型 == 部署类型.Both));
                Map.Instance.decal_高台.SetActive(value && (character.loadData.部署类型 == 部署类型.高台 || character.loadData.部署类型 == 部署类型.Both));
                if (value)
                {
                    Game.Instance.ui_battle.ShowStats(true, character);
                    // Game.Instance.CameraManager.DoRotation(new Vector3(60, 0, -3));
                }
                else
                {
                    Game.Instance.ui_battle.ShowStats(false);
                    // Game.Instance.CameraManager.DoRotation(new Vector3(60, 0, 0));
                }
            }
        }

        partial void Init()
        {
            onDragStart.Add(OnDragStart);
            changeStateOnClick = false;
            draggable = true;
        }

        public void Render(int index)
        {
            idx = index;
            character = Game.Instance.CharacterManager.characters[index];
            icon = character.avatarURLs[character.skinIdx];
            m_职业icon.SetSelectedPage(character.loadData.职业.ToString());
            // m_elite_lv.icon = character.eliteURLs;
            m_cost.text = character.loadData.部署费用.ToString();
        }


        void OnDragStart(EventContext context)
        {
            context.PreventDefault();
            Game.Instance.ui_battle.m_card_list.selectedIndex = idx;
            DragDropManager.inst.StartDrag(this, character.dragImgURLs[character.skinIdx], null);
        }
    }
}