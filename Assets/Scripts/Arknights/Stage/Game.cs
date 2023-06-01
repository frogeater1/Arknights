using UnityEngine;

namespace Arknights
{
    public class Game : Singleton<Game>
    {

        public CharacterManager CharacterManager;
        public PoolManager PoolManager;
        
        public UI_Battle ui_battle;

        protected override void Awake()
        {
            base.Awake();
            ArknightsBinder.BindAll();
            CharacterManager.Init();
            PoolManager.Init();
        }
    }
}