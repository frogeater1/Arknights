/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Button_取消2 : GButton
    {
        public Controller m_button;
        public GGraph m_n0;
        public GGraph m_n1;
        public GGraph m_n2;
        public const string URL = "ui://wnbsox0hnnfh4n";

        public static UI_Button_取消2 CreateInstance()
        {
            return (UI_Button_取消2)UIPackage.CreateObject("Arknights", "Button_取消2");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_n0 = (GGraph)GetChildAt(0);
            m_n1 = (GGraph)GetChildAt(1);
            m_n2 = (GGraph)GetChildAt(2);
            Init();
        }
        partial void Init();
    }
}