using System;
using FairyGUI;
using OnlineGame;
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


        public int logicFrame = 0;

        protected override void Awake()
        {
            base.Awake();
            CameraManager.Init();
            CharacterManager.Init();
            PoolManager.Init();

            Dispacher.SendMsg(new GameStart { Data = 1 });
            EventManager.LogicUpdate += OnLogicUpdate;
        }

        private void OnLogicUpdate()
        {
            logicFrame++;
        }

#if !OUTLINE_TEST
        private void Start()
        {
            //单机调试时模拟

            var t = new System.Timers.Timer();
            t.Interval = 1000f / 60;
            t.Elapsed += (sender, args) => { EventManager.CallLogicUpdate(); };
            t.Enabled = true;
        }
#endif
        private void Update()
        {
            Dispacher.Distribute();
        }
    }
}