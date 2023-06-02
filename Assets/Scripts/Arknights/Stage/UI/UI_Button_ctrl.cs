using FairyGUI;
using UnityEditor.Timeline;
using UnityEngine;

namespace Arknights
{
    public partial class UI_Button_ctrl
    {
        private const float radiusLimit = 185f; //限制离开中心点的半径
        private const float radiusCancel = 100f; //视为选择中间取消的半径
        public Vector2 center;
        public Vector2 startPos;

        partial void Init()
        {
            //私有方法不继承
            onTouchBegin.Add(__touchBegin);
            onTouchMove.Add(__touchMove);
            onTouchEnd.Add(__touchEnd);
        }

        private void __touchBegin(EventContext context)
        {
            InputEvent evt = context.inputEvent;
            startPos = evt.position;
            draggingObject = this;
            // context.CaptureTouch();
        }

        private void __touchMove(EventContext context)
        {
            //移动逻辑
            InputEvent evt = (InputEvent)context.data;
            var move = evt.position - startPos;
            var newPos = center + (move.magnitude < radiusLimit ? move : move.normalized * radiusLimit);
            this.SetXY(Mathf.RoundToInt(newPos.x), Mathf.RoundToInt(newPos.y));

            
            //业务逻辑
            if (move.magnitude < radiusCancel)
            {
                Game.Instance.ui_directionSelect.m_option.selectedPage = "取消";
                Map.Instance.HideAttackRange();
            }
            else if (Vector2.Angle(new Vector2(1, 0), move) < 45f)
            {
                Game.Instance.ui_directionSelect.m_option.selectedPage = "右";
                Map.Instance.ShowAttackRange(0);
            }
            else if (Vector2.Angle(new Vector2(0, 1), move) < 45f)
            {
                Game.Instance.ui_directionSelect.m_option.selectedPage = "下";
                Map.Instance.ShowAttackRange(90);
            }
            else if (Vector2.Angle(new Vector2(-1, 0), move) < 45f)
            {
                Game.Instance.ui_directionSelect.m_option.selectedPage = "左";
                Map.Instance.ShowAttackRange(180);
            }
            else if (Vector2.Angle(new Vector2(0, -1), move) < 45f)
            {
                Game.Instance.ui_directionSelect.m_option.selectedPage = "上";
                Map.Instance.ShowAttackRange(270);
            }
        }

        private void __touchEnd(EventContext context)
        {
            if (draggingObject == this)
            {
                this.SetXY(center.x, center.y);
                draggingObject = null;
            }

            if (Game.Instance.ui_directionSelect.m_option.selectedPage != "取消")
            {
                Map.Instance.curCharacter.下场();
            }
        }
    }
}