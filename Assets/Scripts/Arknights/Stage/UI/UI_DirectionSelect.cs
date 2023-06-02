using FairyGUI;
using UnityEngine;

namespace Arknights
{
    public partial class UI_DirectionSelect
    {
        partial void Init()
        {
            Game.Instance.ui_directionSelect = this;
            visible = false;
            m_option.selectedPage = "取消";
            m_ctrl.center = new Vector2(pivotX * size.x, pivotY * size.y);
        }
        
        public void Show()
        {
            Map.Instance.SetSceneUIPos();
            visible = true;
        }

        public void Hide()
        {
            visible = false;
        }
    }
}