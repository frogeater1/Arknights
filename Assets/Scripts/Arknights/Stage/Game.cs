using System;
using FairyGUI;
using UnityEngine;

namespace Arknights
{
    public class Game : Singleton<Game>
    {
        
        public CameraManager CameraManager;

        public CharacterManager CharacterManager;

        public PoolManager PoolManager;
        
        public AttackRange attackRange;
        
        public HpSpSliders hpSpSliders;

        public UI_Battle ui_battle;
        public UI_DirectionSelect ui_directionSelect;
        public OnlineWindow ui_online_window;
        
        
        public Player me;
            
        public int logicFrame = 0;
        protected override void Awake()
        {
            base.Awake();
            CameraManager.Init();
            CharacterManager.Init(); //延迟到点击创建或加入时再Init
            PoolManager.Init();
            EventManager.LogicUpdate+= OnLogicUpdate;
        }

        private void OnLogicUpdate()
        {
            logicFrame++;
        }


        private void Update()
        {
            Dispacher.Distribute();
        }
    }
}