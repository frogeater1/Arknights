using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using FairyGUI;
using UnityEngine;

namespace Arknights
{
    public class Main : Singleton<Main>
    {
        public Character[] characterPrefabs;

        public UI_Home ui_bg;
        public UI_MainMenu_1 ui_menu1;
        public UI_MainMenu_2 ui_menu2;
        public UI_Loading ui_loading;

        public OnlineWindow ui_online_window;

        //tmp 
        public Player me = new()
        {
            team = Team.Blue,
            name = "未命名"
        };

        protected override void Awake()
        {
            base.Awake();
            ArknightsBinder.BindAll();
        }
        
        private void Update()
        {
            Dispacher.Distribute();
        }
    }
}