/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Button_取消 : GButton
    {
        public Controller m_button;
        public GImage m_n0;
        public const string URL = "ui://wnbsox0hozhr4a";

        public static UI_Button_取消 CreateInstance()
        {
            return (UI_Button_取消)UIPackage.CreateObject("Arknights", "Button_取消");
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