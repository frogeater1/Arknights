/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Button_white : GButton
    {
        public Controller m_button;
        public GImage m_n0;
        public const string URL = "ui://wnbsox0ha0xze";

        public static UI_Button_white CreateInstance()
        {
            return (UI_Button_white)UIPackage.CreateObject("Arknights", "Button_white");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_n0 = (GImage)GetChildAt(0);
            Init();
        }
        partial void Init();
    }
}