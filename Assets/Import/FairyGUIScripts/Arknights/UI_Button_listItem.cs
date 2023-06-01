/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Button_listItem : GButton
    {
        public Controller m_button;
        public GLoader m_icon;
        public GImage m_n6;
        public const string URL = "ui://wnbsox0ha0xz1y";

        public static UI_Button_listItem CreateInstance()
        {
            return (UI_Button_listItem)UIPackage.CreateObject("Arknights", "Button_listItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_icon = (GLoader)GetChildAt(0);
            m_n6 = (GImage)GetChildAt(1);
            Init();
        }
        partial void Init();
    }
}