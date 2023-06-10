/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Button_escape : GButton
    {
        public Controller m_button;
        public GImage m_n3;
        public const string URL = "ui://wnbsox0hnnfh4g";

        public static UI_Button_escape CreateInstance()
        {
            return (UI_Button_escape)UIPackage.CreateObject("Arknights", "Button_escape");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_n3 = (GImage)GetChildAt(0);
            Init();
        }
        partial void Init();
    }
}