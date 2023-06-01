/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Battery : GLabel
    {
        public Controller m_c1;
        public GTextField m_title;
        public GImage m_n1;
        public GImage m_n2;
        public GImage m_n4;
        public GImage m_n5;
        public const string URL = "ui://wnbsox0ha0xz1k";

        public static UI_Battery CreateInstance()
        {
            return (UI_Battery)UIPackage.CreateObject("Arknights", "Battery");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m_title = (GTextField)GetChildAt(0);
            m_n1 = (GImage)GetChildAt(1);
            m_n2 = (GImage)GetChildAt(2);
            m_n4 = (GImage)GetChildAt(3);
            m_n5 = (GImage)GetChildAt(4);
            Init();
        }
        partial void Init();
    }
}