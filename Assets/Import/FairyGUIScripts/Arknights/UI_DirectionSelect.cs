/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_DirectionSelect : GComponent
    {
        public Controller m_option;
        public UI_Button_取消2 m_n29;
        public GImage m_n5;
        public GImage m_n8;
        public GImage m_n9;
        public GImage m_n10;
        public UI_Button_取消 m_n22;
        public GImage m_n0;
        public UI_Button_ctrl m_ctrl;
        public UI_Skill m_skill;
        public UI_Button_escape m_n31;
        public const string URL = "ui://wnbsox0hhl0g42";

        public static UI_DirectionSelect CreateInstance()
        {
            return (UI_DirectionSelect)UIPackage.CreateObject("Arknights", "DirectionSelect");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_option = GetControllerAt(0);
            m_n29 = (UI_Button_取消2)GetChildAt(0);
            m_n5 = (GImage)GetChildAt(1);
            m_n8 = (GImage)GetChildAt(2);
            m_n9 = (GImage)GetChildAt(3);
            m_n10 = (GImage)GetChildAt(4);
            m_n22 = (UI_Button_取消)GetChildAt(5);
            m_n0 = (GImage)GetChildAt(6);
            m_ctrl = (UI_Button_ctrl)GetChildAt(7);
            m_skill = (UI_Skill)GetChildAt(8);
            m_n31 = (UI_Button_escape)GetChildAt(9);
            Init();
        }
        partial void Init();
    }
}