/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Battle : GComponent
    {
        public Controller m_c1;
        public UI_Button_设置 m_setting;
        public UI_Button_速度 m_speed;
        public UI_Button_播放 m_playOrPause;
        public GImage m_n5;
        public GImage m_n3;
        public GImage m_n6;
        public GTextField m_n7;
        public GTextField m_n8;
        public GTextField m_n9;
        public GTextField m_n10;
        public GGraph m_n14;
        public GGraph m_n15;
        public UI_ProgressBar1 m_n18;
        public GImage m_n20;
        public GTextField m_n21;
        public GTextField m_n23;
        public GTextField m_n22;
        public GLoader m_icon;
        public GImage m_n26;
        public GGroup m_stats;
        public GList m_card_list;
        public const string URL = "ui://wnbsox0hoeh32e";

        public static UI_Battle CreateInstance()
        {
            return (UI_Battle)UIPackage.CreateObject("Arknights", "Battle");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m_setting = (UI_Button_设置)GetChildAt(0);
            m_speed = (UI_Button_速度)GetChildAt(1);
            m_playOrPause = (UI_Button_播放)GetChildAt(2);
            m_n5 = (GImage)GetChildAt(3);
            m_n3 = (GImage)GetChildAt(4);
            m_n6 = (GImage)GetChildAt(5);
            m_n7 = (GTextField)GetChildAt(6);
            m_n8 = (GTextField)GetChildAt(7);
            m_n9 = (GTextField)GetChildAt(8);
            m_n10 = (GTextField)GetChildAt(9);
            m_n14 = (GGraph)GetChildAt(10);
            m_n15 = (GGraph)GetChildAt(11);
            m_n18 = (UI_ProgressBar1)GetChildAt(12);
            m_n20 = (GImage)GetChildAt(13);
            m_n21 = (GTextField)GetChildAt(14);
            m_n23 = (GTextField)GetChildAt(15);
            m_n22 = (GTextField)GetChildAt(16);
            m_icon = (GLoader)GetChildAt(17);
            m_n26 = (GImage)GetChildAt(18);
            m_stats = (GGroup)GetChildAt(19);
            m_card_list = (GList)GetChildAt(20);
            Init();
        }
        partial void Init();
    }
}