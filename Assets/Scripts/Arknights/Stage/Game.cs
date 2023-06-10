using System;
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
        
        //tmp
        public int logicFrame = 0;
        
        
        protected override void Awake()
        {
            base.Awake();
            ArknightsBinder.BindAll();
            CameraManager.Init();
            CharacterManager.Init();
            PoolManager.Init();
            EventManager.LogicUpdate+= OnLogicUpdate;
        }

        private void OnLogicUpdate()
        {
            logicFrame++;
        }


        //tmp 
        private void Update()
        {
            EventManager.CallLogicUpdate();
        }
    }
}