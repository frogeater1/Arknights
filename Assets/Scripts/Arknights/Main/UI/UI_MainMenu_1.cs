using FairyGUI;
using UnityEngine;

namespace Arknights
{
    public partial class UI_MainMenu_1
    {
        partial void Init()
        {
            // Main.Instance.ui_menu1 = this;

            // Stage.inst.onClick.Add(() =>
            // {
            //     Debug.Log(GRoot.inst.touchTarget.name);
            // });
            m_Terminal.onClick.Add(OnClickTerminal);
        }

        void OnClickTerminal()
        {
            //todo: 进入Team界面选人
            // Window win = new();
            // // win.modal = true;
            // win.contentPane = UI_Server.CreateInstance();
            // win.Show();
            //
            
            
            // Stage.inst.gameObject.
            // Debug.Log();
            // win.SetPosition(1920 / 2 - 600 / 2, 1080 / 2 - 400 / 2, 0);
            // win.SetPosition(200, 100, 0);
            // Main.Instance.onlineWindow = win;
            Parking.StartBattle(Main.Instance.characterPrefabs);
        }
    }
}