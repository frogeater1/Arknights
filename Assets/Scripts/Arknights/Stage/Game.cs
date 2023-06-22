using System;
using System.Timers;
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

        public Player me;

        public int logicFrame = 0;

        protected override void Awake()
        {
            base.Awake();
            CameraManager.Init();
            CharacterManager.Init();
            PoolManager.Init();

            me = Parking.room.players[Parking.meId - 1];

            Dispacher.SendMsg(new GameStart { Data = 1 });
        }
        
#if OUTLINE_TEST
        //单机调试时模拟
        public Timer t;
        private void OnDisable()
        {
            t.Close();
        }
        private void Start()
        {
            t = new Timer();
            t.Interval = 1000f / Settings.FPS;
            t.Elapsed += (sender, args) =>
            {
                RpcMsg[] rpcs;
                lock (Dispacher.rpcMsgs)
                {
                    rpcs = (Dispacher.rpcMsgs.ToArray());
                    Dispacher.rpcMsgs.Clear();
                }

                Dispacher.logicUpdateMsgs.Enqueue(new LogicUpdate()
                {
                    Rpcs = { rpcs },
                });
            };

            t.Enabled = true;
        }
#endif
        private void Update()
        {
            Dispacher.Distribute();
        }
    }
}