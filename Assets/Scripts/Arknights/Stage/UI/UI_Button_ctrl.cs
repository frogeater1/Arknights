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

        private bool moving;

        public 方向 oldDir;

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
            oldDir = 方向.取消;
            moving = false;
        }

        private void __touchMove(EventContext context)
        {
            //移动逻辑
            moving = true;
            InputEvent evt = (InputEvent)context.data;
            var move = evt.position - startPos;
            var newPos = center + (move.magnitude < radiusLimit ? move : move.normalized * radiusLimit);
            this.SetXY(Mathf.RoundToInt(newPos.x), Mathf.RoundToInt(newPos.y));


            //业务逻辑
            方向 dir = 方向.取消;
            if (move.magnitude < radiusCancel)
            {
                dir = 方向.取消;
            }
            else if (Vector2.Angle(new Vector2(1, 0), move) < 45f)
            {
                dir = 方向.右;
            }
            else if (Vector2.Angle(new Vector2(0, 1), move) < 45f)
            {
                dir = 方向.下;
            }
            else if (Vector2.Angle(new Vector2(-1, 0), move) < 45f)
            {
                dir = 方向.左;
            }
            else if (Vector2.Angle(new Vector2(0, -1), move) < 45f)
            {
                dir = 方向.上;
            }

            if (dir != oldDir)
            {
                oldDir = dir;
                Game.Instance.CharacterManager.curCharacter.ChangeDirection(dir);
                EventManager.CallChangeDirection(dir);
            }
        }

        private void __touchEnd(EventContext context)
        {
            if (!moving) return;
            moving = false;
            if (draggingObject == this)
            {
                this.SetXY(center.x, center.y);
                draggingObject = null;
            }
            
            if (oldDir != 方向.取消)
            {
                EventManager.CallCancelSelect();
                Commander.Enter(Game.Instance.CharacterManager.curCharacter);
                Game.Instance.ui_battle.下场();
                Game.Instance.CharacterManager.curCharacter = null;
            }
        }
    }
}