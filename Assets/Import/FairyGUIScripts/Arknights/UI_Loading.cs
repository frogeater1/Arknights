/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Loading : GComponent
    {
        public GLoader m_bg;
        public GTextField m_n1;
        public GTextField m_level;
        public GTextField m_title;
        public GTextField m_tips;
        public GTextField m_txt_loading;
        public GGroup m_stage;
        public GGraph m_curtain;
        public const string URL = "ui://wnbsox0hoeh33v";

        public static UI_Loading CreateInstance()
        {
            return (UI_Loading)UIPackage.CreateObject("Arknights", "Loading");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GLoader)GetChildAt(0);
            m_n1 = (GTextField)GetChildAt(1);
            m_level = (GTextField)GetChildAt(2);
            m_title = (GTextField)GetChildAt(3);
            m_tips = (GTextField)GetChildAt(4);
            m_txt_loading = (GTextField)GetChildAt(5);
            m_stage = (GGroup)GetChildAt(6);
            m_curtain = (GGraph)GetChildAt(7);
            Init();
        }
        partial void Init();
    }
}