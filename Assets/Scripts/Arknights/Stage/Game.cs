using System;
using UnityEngine;

namespace Arknights
{
    public class Game : Singleton<Game>
    {
        public CameraManager CameraManager;

        public CharacterManager CharacterManager;

        public PoolManager PoolManager;

        public UI_Battle ui_battle;
        public UI_DirectionSelect ui_directionSelect;

        public Character curCharacter
        {
            get => ui_battle.m_card_list.selectedIndex == -1 ? null : ((UI_Button_角色卡)ui_battle.m_card_list.GetChildAt(ui_battle.m_card_list.selectedIndex)).character;
        }
        
        protected override void Awake()
        {
            base.Awake();
            ArknightsBinder.BindAll();
            CameraManager.Init();
            CharacterManager.Init();
            PoolManager.Init();
        }
    }
}