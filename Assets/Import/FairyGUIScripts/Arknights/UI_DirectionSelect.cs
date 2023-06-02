/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_DirectionSelect : GComponent
    {
        public Controller m_option;
        public Controller m_button;
        public GImage m_n0;
        public GImage m_n5;
        public GImage m_n8;
        public GImage m_n9;
        public GImage m_n10;
        public UI_Button_ctrl m_ctrl;
        public UI_Button_取消 m_n22;
        public const string URL = "ui://wnbsox0hhl0g42";

        public static UI_DirectionSelect CreateInstance()
        {
            return (UI_DirectionSelect)UIPackage.CreateObject("Arknights", "DirectionSelect");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_option = GetControllerAt(0);
            m_button = GetControllerAt(1);
            m_n0 = (GImage)GetChildAt(0);
            m_n5 = (GImage)GetChildAt(1);
            m_n8 = (GImage)GetChildAt(2);
            m_n9 = (GImage)GetChildAt(3);
            m_n10 = (GImage)GetChildAt(4);
            m_ctrl = (UI_Button_ctrl)GetChildAt(5);
            m_n22 = (UI_Button_取消)GetChildAt(6);
            Init();
        }
        partial void Init();
    }
}