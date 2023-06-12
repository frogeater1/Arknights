using FairyGUI;
using UnityEngine;

namespace Arknights
{
    public partial class UI_MainMenu_1
    {
        partial void Init()
        {
            Main.Instance.ui_menu1 = this;
            Main.Instance.ui_online_window  = new OnlineWindow();
            m_Terminal.onClick.Add(OnClickTerminal);
        }

        void OnClickTerminal()
        {
            //todo: 进入Team界面选人
            Main.Instance.ui_online_window.Show();
            
            // Parking.StartBattle(Main.Instance.characterPrefabs);
        }
    }
}