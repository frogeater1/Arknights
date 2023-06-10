/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Skill : GComponent
    {
        public Controller m_ready;
        public UI_Button_Skill m_skill_button;
        public GImage m_n1;
        public GTextField m_electricity;
        public GImage m_ready_2;
        public GGraph m_n6;
        public GImage m_n5;
        public Transition m_t0;
        public const string URL = "ui://wnbsox0hnnfh4k";

        public static UI_Skill CreateInstance()
        {
            return (UI_Skill)UIPackage.CreateObject("Arknights", "Skill");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_ready = GetControllerAt(0);
            m_skill_button = (UI_Button_Skill)GetChildAt(0);
            m_n1 = (GImage)GetChildAt(1);
            m_electricity = (GTextField)GetChildAt(2);
            m_ready_2 = (GImage)GetChildAt(3);
            m_n6 = (GGraph)GetChildAt(4);
            m_n5 = (GImage)GetChildAt(5);
            m_t0 = GetTransitionAt(0);
            Init();
        }
        partial void Init();
    }
}