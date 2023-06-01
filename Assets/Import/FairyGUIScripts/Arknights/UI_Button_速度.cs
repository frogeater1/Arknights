/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Button_速度 : GButton
    {
        public Controller m_button;
        public Controller m_speed;
        public GImage m_n3;
        public GImage m_n4;
        public GImage m_n5;
        public const string URL = "ui://wnbsox0hoeh32g";

        public static UI_Button_速度 CreateInstance()
        {
            return (UI_Button_速度)UIPackage.CreateObject("Arknights", "Button_速度");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_speed = GetControllerAt(1);
            m_n3 = (GImage)GetChildAt(0);
            m_n4 = (GImage)GetChildAt(1);
            m_n5 = (GImage)GetChildAt(2);
            Init();
        }
        partial void Init();
    }
}