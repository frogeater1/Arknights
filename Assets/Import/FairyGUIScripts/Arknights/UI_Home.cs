/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Home : GComponent
    {
        public GImage m_n8;
        public GImage m_n21;
        public GImage m_n18;
        public GImage m_n19;
        public GImage m_n20;
        public GTextField m_n22;
        public GTextField m_n24;
        public GTextField m_n25;
        public GGroup m_n27;
        public GImage m_n28;
        public GImage m_n11;
        public GImage m_n12;
        public GImage m_n13;
        public GImage m_n14;
        public GGroup m_n29;
        public const string URL = "ui://wnbsox0ha0xz0";

        public static UI_Home CreateInstance()
        {
            return (UI_Home)UIPackage.CreateObject("Arknights", "Home");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_n8 = (GImage)GetChildAt(0);
            m_n21 = (GImage)GetChildAt(1);
            m_n18 = (GImage)GetChildAt(2);
            m_n19 = (GImage)GetChildAt(3);
            m_n20 = (GImage)GetChildAt(4);
            m_n22 = (GTextField)GetChildAt(5);
            m_n24 = (GTextField)GetChildAt(6);
            m_n25 = (GTextField)GetChildAt(7);
            m_n27 = (GGroup)GetChildAt(8);
            m_n28 = (GImage)GetChildAt(9);
            m_n11 = (GImage)GetChildAt(10);
            m_n12 = (GImage)GetChildAt(11);
            m_n13 = (GImage)GetChildAt(12);
            m_n14 = (GImage)GetChildAt(13);
            m_n29 = (GGroup)GetChildAt(14);
            Init();
        }
        partial void Init();
    }
}