/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Button_close : GButton
    {
        public Controller m_button;
        public GTextField m_n0;
        public const string URL = "ui://wnbsox0h9stu4t";

        public static UI_Button_close CreateInstance()
        {
            return (UI_Button_close)UIPackage.CreateObject("Arknights", "Button_close");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_n0 = (GTextField)GetChildAt(0);
            Init();
        }
        partial void Init();
    }
}