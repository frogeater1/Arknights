using FairyGUI;
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