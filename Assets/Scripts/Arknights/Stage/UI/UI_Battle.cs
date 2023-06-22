using FairyGUI;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.TextCore.Text;

namespace Arknights
{
    public partial class UI_Battle
    {
        partial void Init()
        {
            Game.Instance.ui_battle = this;

            m_stats.visible = false;
            m_card_list.itemRenderer = RenderCard;
            m_card_list.numItems = 2;
            m_card_list.ResizeToFit();
            
            m_card_list.onClickItem.Add(__clickCard);
        }

        private void __clickCard(EventContext context)
        {
            Debug.Log(context.data);
        }

        public void CancelSelect()
        {
            m_card_list.selectedIndex = -1;
        }

        public void 进场()
        {
            UI_Button_角色卡 button = (UI_Button_角色卡)m_card_list.GetChildAt(m_card_list.selectedIndex);
            button.visible = false;
            m_card_list.selectedIndex = -1;
        }

        public void 回收(int cardListIdx)
        {
            UI_Button_角色卡 button = (UI_Button_角色卡)m_card_list.GetChildAt(cardListIdx);
            button.visible = true;
        }

        private void RenderCard(int index, GObject obj)
        {
            UI_Button_角色卡 button = (UI_Button_角色卡)obj;
            button.Render(index);
        }

        public void ShowStats(bool show, Character character = null)
        {
            m_stats.visible = show;
            if (character)
            {
                m_icon.icon = character.tachieURLs[character.skinIdx];
                //todo: 角色数据展示
            }
        }
    }
}