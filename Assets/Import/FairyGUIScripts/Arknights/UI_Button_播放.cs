/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Button_播放 : GButton
    {
        public Controller m_button;
        public Controller m_c1;
        public GImage m_n3;
        public GImage m_n4;
        public const string URL = "ui://wnbsox0hoeh32i";

        public static UI_Button_播放 CreateInstance()
        {
            return (UI_Button_播放)UIPackage.CreateObject("Arknights", "Button_播放");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_c1 = GetControllerAt(1);
            m_n3 = (GImage)GetChildAt(0);
            m_n4 = (GImage)GetChildAt(1);
            Init();
        }
        partial void Init();
    }
}