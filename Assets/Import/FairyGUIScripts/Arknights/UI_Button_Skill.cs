/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Button_Skill : GButton
    {
        public Controller m_button;
        public GLoader m_icon;
        public const string URL = "ui://wnbsox0hnnfh4h";

        public static UI_Button_Skill CreateInstance()
        {
            return (UI_Button_Skill)UIPackage.CreateObject("Arknights", "Button_Skill");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_icon = (GLoader)GetChildAt(0);
            Init();
        }
        partial void Init();
    }
}