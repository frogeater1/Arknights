using Arknights.Skills;
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
                    var character = Game.Instance.CharacterManager.curCharacter;
                    ScenenUI.position = new Vector3(character.transform.localPosition.x, 1f,
                        Mathf.Floor(character.transform.localPosition.z) + 0.5f);
                    EventManager.ChangeDirection += OnChangeDirection;
                    EventManager.CancelSelect += Hide;
                }
                else
                {
                    EventManager.ChangeDirection -= OnChangeDirection;
                    EventManager.CancelSelect -= Hide;
                }
            }
        }

        private void OnChangeDirection(Character character, 方向 direction)
        {
            m_option.selectedPage = direction.ToString();
        }

        private void Hide()
        {
            visible = false;
        }

        partial void Init()
        {
            ScenenUI = GameObject.Find("SceneUI").transform;
            Game.Instance.ui_directionSelect = this;
            visible = false;
            m_option.selectedPage = 方向.取消.ToString();
            m_ctrl.center = new Vector2(pivotX * size.x, pivotY * size.y);
        }

        public void ShowCtrl(Character character)
        {
            visible = true;
            m_option.SetSelectedPage(方向.操作.ToString());
            m_skill.Show(character);
        }
        
    }
}