using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace Arknights
{
    public partial class UI_MainMenu_1
    {
        partial void Init()
        {
            Main.Instance.ui_menu1 = this;
            m_Terminal.onClick.Add(OnClickTerminal);
        }

        void OnClickTerminal()
        {
            Main.Instance.ui_online_window = new OnlineWindow();
            Main.Instance.ui_online_window.Show();
            //todo: 进入Team界面选人,这里直接写死
            Main.Instance.me.selectCardIdxs = new List<int> { 0, 1 };
        }
    }
}