using FairyGUI;
using UnityEngine;

namespace Arknights
{
    public class OnlineWindow : Window
    {
        protected override void OnInit()
        {
            contentPane = UI_Server.CreateInstance();
            //居中
            SetXY(Screen.width / 2 - contentPane.width / 2, Screen.height / 2 - contentPane.height / 2);
            
            modal = true;
        }
    }
}