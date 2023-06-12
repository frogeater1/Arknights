using System;
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
        
        protected override void Awake()
        {
            base.Awake();
            ArknightsBinder.BindAll();
        }
    }
}