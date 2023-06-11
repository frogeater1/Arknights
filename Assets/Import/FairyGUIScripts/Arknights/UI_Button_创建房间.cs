/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Button_创建房间 : GButton
    {
        public Controller m_button;
        public GGraph m_n0;
        public GGraph m_n1;
        public GGraph m_n2;
        public GTextField m_n3;
        public const string URL = "ui://wnbsox0h9stu4p";

        public static UI_Button_创建房间 CreateInstance()
        {
            return (UI_Button_创建房间)UIPackage.CreateObject("Arknights", "Button_创建房间");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_n0 = (GGraph)GetChildAt(0);
            m_n1 = (GGraph)GetChildAt(1);
            m_n2 = (GGraph)GetChildAt(2);
            m_n3 = (GTextField)GetChildAt(3);
            Init();
        }
        partial void Init();
    }
}