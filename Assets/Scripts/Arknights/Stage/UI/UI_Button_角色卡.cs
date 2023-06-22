using System.Globalization;
using FairyGUI;
using UnityEditor.Timeline;
using UnityEngine;

namespace Arknights
{
    public partial class UI_Button_角色卡
    {
        public int idx;
        public Character character;

        private Vector2 origin;
        private Vector2 startPos;

        private bool canSet;

        private bool moving;

        private bool canTouch;
        // private Vector2Int oldGridPos = new(-1, -1); //缓存上一次的格子坐标，如果没变就不做后面的操作了

        public override bool selected
        {
            get { return base.selected; }
            set
            {
                base.selected = value;
                Map.Instance.decal_地面.SetActive(value && character.loadData.部署类型 is 部署类型.地面 or 部署类型.Both);
                Map.Instance.decal_高台.SetActive(value && character.loadData.部署类型 is 部署类型.高台 or 部署类型.Both);
                Game.Instance.CharacterManager.curCharacter = value ? this.character : null;
            }
        }

        partial void Init()
        {
            onTouchBegin.Set(__touchBegin);
            onTouchMove.Set(__touchMove);
            onTouchEnd.Set(__touchEnd);
            canTouch = true;


            changeStateOnClick = false;
        }

        public void Render(int index)
        {
            idx = index;
            character = Game.Instance.CharacterManager.characters[Parking.meId - 1][index];
            character.cardListIdx = idx;
            icon = character.avatarURLs[character.skinIdx];
            m_职业icon.SetSelectedPage(character.loadData.职业.ToString());
            // m_elite_lv.icon = character.eliteURLs;
            m_cost.text = character.loadData.部署费用.ToString();

            origin = m_drager.TransformPoint(new Vector2(-15, -15), Game.Instance.ui_battle);
        }

        private void __touchBegin(EventContext context)
        {
            if (!canTouch) return;
            InputEvent evt = context.inputEvent;
            startPos = evt.position;
            var startXY = startPos - origin;
            m_drager.SetXY(startXY.x, startXY.y);
            m_drager.url = character.dragImgURLs[character.skinIdx];
            m_drager.visible = true;
            moving = false;
        }

        private void __touchMove(EventContext context)
        {
            if (!canTouch) return;
            moving = true;
            //拖拽逻辑
            InputEvent evt = context.inputEvent;
            Vector2 move = evt.position - startPos;
            m_drager.SetXY(Mathf.RoundToInt(move.x), Mathf.RoundToInt(move.y));


            //业务逻辑
            Game.Instance.ui_battle.m_card_list.selectedIndex = idx;
            var ray = Game.Instance.CameraManager.mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out var hit);
            Vector3 pos =
                Game.Instance.CameraManager.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, hit.distance));

            canSet = false;
            if (pos.y < 10) //用这个判断是否打到可部署范围内，因为非可部署范围没有collider,会返回相机附近的位置。
            {
                var setType = character.loadData.部署类型;
                var logic_x = Mathf.FloorToInt(pos.x);
                var logic_z = Mathf.FloorToInt(pos.z);
                // if (fixed_x == oldGridPos.x && fixed_z == oldGridPos.y)
                // {
                //     //暂时不缓存优化，会让逻辑不清晰
                //     return;
                // }
                var grid_type = Map.Instance.GetGrid(logic_x, logic_z)?.type;
                //修正生成位置为格子正中间
                if ((setType == 部署类型.地面 && grid_type == GridType.站人地面)
                    || (setType == 部署类型.高台 && grid_type == GridType.站人高台)
                    || (setType == 部署类型.Both && (grid_type == GridType.站人地面 || grid_type == GridType.站人高台)))
                {
                    m_drager.visible = false;
                    character.FixedPos(logic_x, logic_z);
                    canSet = true;
                    Game.Instance.attackRange.Show();
                }
            }

            if (!canSet)
            {
                m_drager.visible = true;
                character.transform.position = new Vector3(1000, 0, 0);
                Game.Instance.attackRange.Hide();
            }
        }

        private void __touchEnd(EventContext context)
        {
            if (!canTouch) return;
            m_drager.SetXY(origin.x, origin.y);
            m_drager.visible = false;
            Game.Instance.attackRange.Hide();
            if (canSet)
            {
                Game.Instance.ui_directionSelect.visible = true;
                Game.Instance.ui_directionSelect.m_option.SetSelectedPage(方向.取消.ToString());
            }
            else
            {
                Game.Instance.ui_directionSelect.visible = false;
                if (moving) //这里是为了解决和click同时触发的问题
                {
                    moving = false;
                    Game.Instance.CharacterManager.curCharacter = null;
                    Game.Instance.ui_battle.m_card_list.selectedIndex = -1;
                }
            }
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (character.lastCoolDown > 0)
            {
                m_cooldowntext.visible = true;
                m_cooldowntext.text = character.lastCoolDown.ToString();
                canTouch = false;
            }
            else
            {
                m_cooldowntext.visible = false;
                canTouch = true;
            }

            m_cooldown.fillAmount = character.lastCoolDown / character.coolDown;
        }
    }
}