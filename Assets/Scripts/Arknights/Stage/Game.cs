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

        // public Team team;
        
        protected override void Awake()
        {
            base.Awake();
            ArknightsBinder.BindAll();
            CameraManager.Init();
            CharacterManager.Init();
            PoolManager.Init();
        }
        
        
        //tmp 
        private void Update()
        {
            EventManager.CallLogicUpdate();
        }
    }
}