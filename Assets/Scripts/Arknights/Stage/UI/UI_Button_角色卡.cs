using FairyGUI;
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

        public override bool selected
        {
            get { return base.selected; }
            set
            {
                base.selected = value;
                Map.Instance.decal_地面.SetActive(value && (character.loadData.部署类型 == 部署类型.地面 || character.loadData.部署类型 == 部署类型.Both));
                Map.Instance.decal_高台.SetActive(value && (character.loadData.部署类型 == 部署类型.高台 || character.loadData.部署类型 == 部署类型.Both));
                if (value)
                {
                    Game.Instance.ui_battle.ShowStats(true, character);
                    Game.Instance.CameraManager.DoRotation(new Vector3(60, 0, -3));
                     
                    Map.Instance.curCharacter = character;
                }
                else
                {
                    Game.Instance.ui_battle.ShowStats(false);
                    Game.Instance.CameraManager.DoRotation(new Vector3(60, 0, 0));
                    
                    Map.Instance.curCharacter = null;
                }
            }
        }

        partial void Init()
        {
            onTouchBegin.Add(__touchBegin);
            onTouchMove.Add(__touchMove);
            onTouchEnd.Add(__touchEnd);
            changeStateOnClick = false;
        }

        public void Render(int index)
        {
            idx = index;
            character = Game.Instance.CharacterManager.characters[index];
            icon = character.avatarURLs[character.skinIdx];
            m_职业icon.SetSelectedPage(character.loadData.职业.ToString());
            // m_elite_lv.icon = character.eliteURLs;
            m_cost.text = character.loadData.部署费用.ToString();
            
            origin =   m_drager.TransformPoint(new Vector2(-15,-15),Game.Instance.ui_battle);
        }

        private void __touchBegin(EventContext context)
        {
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
            Debug.Log("moving");
            moving = true;
            //拖拽逻辑
            InputEvent evt = context.inputEvent;
            Vector2 move = evt.position - startPos;
            m_drager.SetXY(Mathf.RoundToInt(move.x), Mathf.RoundToInt(move.y));


            //业务逻辑
            Game.Instance.ui_battle.m_card_list.selectedIndex = idx;
            var ray = Game.Instance.CameraManager.mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out var hit);
            Vector3 pos = Game.Instance.CameraManager.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, hit.distance));
            //修正生成位置为格子正中间
            canSet = false;
            if (pos.y < 10) //用这个判断是否打到可部署范围内，因为非可部署范围没有collider,会返回相机附近的位置。
            {
                var setType = character.loadData.部署类型;
                var fixed_x = Mathf.FloorToInt(pos.x);
                var fixed_z = Mathf.FloorToInt(pos.z);
                var pos_fixed = new Vector3(fixed_x + 0.5f, 0, fixed_z + 0.2f);
                var grid_type = Map.Instance.GetGridType(fixed_x, fixed_z);
                if ((setType == 部署类型.地面 && grid_type == GridType.站人地面)
                    || (setType == 部署类型.高台 && grid_type == GridType.站人高台)
                    || (setType == 部署类型.Both && (grid_type == GridType.站人地面 || grid_type == GridType.站人高台)))
                {
                    m_drager.visible = false;
                    character.transform.position = pos_fixed;
                    canSet = true;
                    Map.Instance.ShowAttackRange();
                }
            }
            
            if (!canSet)
            {
                m_drager.visible = true;
                character.transform.position = new Vector3(1000, 0, 0);
                Map.Instance.HideAttackRange();
            }
            
            // Game.Instance.CameraManager.mainCamera.ScreenToWorldPoint(new Vector3())
        }

        private void __touchEnd(EventContext context)
        {
            m_drager.SetXY(origin.x,origin.y);
            m_drager.visible = false;
            Map.Instance.HideAttackRange();
            if (canSet)
            {
                Game.Instance.ui_directionSelect.Show();
            }
            else
            {
                Game.Instance.ui_directionSelect.Hide();
                if (moving)
                {
                    Game.Instance.ui_battle.m_card_list.selectedIndex = -1;
                }
            }
        }
    }
}