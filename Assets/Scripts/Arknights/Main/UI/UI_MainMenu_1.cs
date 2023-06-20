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
            m_Terminal.onClick.Set(OnClickTerminal);
            m_Team.onClick.Set(OnClickTeam);
        }

        void OnClickTerminal()
        {
            Main.Instance.ui_online_window = new OnlineWindow();
            Main.Instance.ui_online_window.Show();
            //todo: 进入Team界面选人,这里直接写死
            Main.Instance.me.selectCardIdxs = new List<int> { 0, 1 };
        }

        void OnClickTeam()
        {
            //组队按钮暂时用作单机测试按钮
#if OUTLINE_TEST
            Main.Instance.me.selectCardIdxs = new List<int> { 0, 1 };

            Parking.StartBattle(new Player
            {
                playerId = 1,
                team = Team.Blue,
                name = "me",
                selectCardIdxs = new List<int> { 0, 1 }
            }, new Player
            {
                playerId = 2,
                team = Team.Red,
                name = "enemy",
                selectCardIdxs = new List<int> { 0, 1 }
            }, 1);

#endif
        }
    }
}