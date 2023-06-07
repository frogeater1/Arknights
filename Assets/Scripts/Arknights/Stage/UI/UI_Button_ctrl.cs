using FairyGUI;
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
                EventManager.CallChangeDirection( 方向.取消);
            }
            else if (Vector2.Angle(new Vector2(1, 0), move) < 45f)
            {
                EventManager.CallChangeDirection(方向.右);
            }
            else if (Vector2.Angle(new Vector2(0, 1), move) < 45f)
            {
                EventManager.CallChangeDirection(方向.下);
            }
            else if (Vector2.Angle(new Vector2(-1, 0), move) < 45f)
            {
                EventManager.CallChangeDirection(方向.左);
            }
            else if (Vector2.Angle(new Vector2(0, -1), move) < 45f)
            {
                EventManager.CallChangeDirection(方向.上);
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
                Game.Instance.curCharacter.下场();
                //要先下场,否则选中卡片会被清除后找不到当前角色
                EventManager.CallCancelSelect();
            }
        }
    }
}