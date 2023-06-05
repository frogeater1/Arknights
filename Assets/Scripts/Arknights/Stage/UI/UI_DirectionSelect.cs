using FairyGUI;
using UnityEngine;

namespace Arknights
{
    public partial class UI_DirectionSelect
    {

        public Transform ScenenUI;
        public override bool visible
        {
            get { return base.visible; }
            set
            {
                base.visible = value;
                if (visible)
                {
                    var character = Game.Instance.curCharacter; 
                    ScenenUI.position = new Vector3(character.transform.localPosition.x, 1f, Mathf.Floor(character.transform.localPosition.z) + 0.5f);
                    EventManager.ChangeDirection += OnChangeDirection;
                    EventManager.CancelSelect += OnCancelSelect;
                }
                else
                {
                    EventManager.ChangeDirection -= OnChangeDirection;
                    EventManager.CancelSelect -= OnCancelSelect;
                }
            }
        }

        private void OnChangeDirection(方向 direction)
        {
            m_option.selectedPage = direction.ToString();
        }
        
        private void OnCancelSelect()
        {
            visible = false;
        }

        partial void Init()
        {
            ScenenUI = GameObject.Find("SceneUI").transform;
            Game.Instance.ui_directionSelect = this;
            visible = false;
            m_option.selectedPage = "取消";
            m_ctrl.center = new Vector2(pivotX * size.x, pivotY * size.y);
        }
    }
}