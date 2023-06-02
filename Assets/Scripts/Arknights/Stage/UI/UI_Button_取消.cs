using UnityEngine;

namespace Arknights
{
    partial class UI_Button_取消
    {
        partial void Init()
        {
            onClick.Add(Click);
        }
        
        private void Click()
        {
            Game.Instance.ui_battle.m_card_list.selectedIndex = -1;
            Game.Instance.ui_directionSelect.Hide();
        }
    }
}