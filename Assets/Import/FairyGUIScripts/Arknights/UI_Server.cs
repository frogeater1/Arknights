/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Server : GComponent
    {
        public Controller m_state;
        public GGraph m_n6;
        public UI_Button_创建房间 m_n2;
        public GTextField m_n3;
        public GTextInput m_n4;
        public UI_Button_加入房间 m_n5;
        public GTextField m_n7;
        public UI_Button_取消等待 m_n9;
        public GTextField m_n10;
        public UI_Button_close m_n11;
        public GTextField m_n13;
        public const string URL = "ui://wnbsox0h9stu4o";

        public static UI_Server CreateInstance()
        {
            return (UI_Server)UIPackage.CreateObject("Arknights", "Server");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_state = GetControllerAt(0);
            m_n6 = (GGraph)GetChildAt(0);
            m_n2 = (UI_Button_创建房间)GetChildAt(1);
            m_n3 = (GTextField)GetChildAt(2);
            m_n4 = (GTextInput)GetChildAt(3);
            m_n5 = (UI_Button_加入房间)GetChildAt(4);
            m_n7 = (GTextField)GetChildAt(5);
            m_n9 = (UI_Button_取消等待)GetChildAt(6);
            m_n10 = (GTextField)GetChildAt(7);
            m_n11 = (UI_Button_close)GetChildAt(8);
            m_n13 = (GTextField)GetChildAt(9);
            Init();
        }
        partial void Init();
    }
}