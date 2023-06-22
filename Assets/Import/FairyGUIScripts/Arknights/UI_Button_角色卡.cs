/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Button_角色卡 : GButton
    {
        public Controller m_button;
        public Controller m_职业icon;
        public Controller m_专精lv;
        public GLoader m_drager;
        public GLoader m_icon;
        public GLoader m_n15;
        public GImage m_n17;
        public GTextField m_cost;
        public GGraph m_n20;
        public GImage m_n27;
        public GImage m_n23;
        public GImage m_n24;
        public GImage m_n11;
        public GLoader m_elite_lv;
        public GImage m_cooldown;
        public GTextField m_cooldowntext;
        public GGroup m_n36;
        public const string URL = "ui://wnbsox0hoeh33g";

        public static UI_Button_角色卡 CreateInstance()
        {
            return (UI_Button_角色卡)UIPackage.CreateObject("Arknights", "Button_角色卡");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_button = GetControllerAt(0);
            m_职业icon = GetControllerAt(1);
            m_专精lv = GetControllerAt(2);
            m_drager = (GLoader)GetChildAt(0);
            m_icon = (GLoader)GetChildAt(1);
            m_n15 = (GLoader)GetChildAt(2);
            m_n17 = (GImage)GetChildAt(3);
            m_cost = (GTextField)GetChildAt(4);
            m_n20 = (GGraph)GetChildAt(5);
            m_n27 = (GImage)GetChildAt(6);
            m_n23 = (GImage)GetChildAt(7);
            m_n24 = (GImage)GetChildAt(8);
            m_n11 = (GImage)GetChildAt(9);
            m_elite_lv = (GLoader)GetChildAt(10);
            m_cooldown = (GImage)GetChildAt(11);
            m_cooldowntext = (GTextField)GetChildAt(12);
            m_n36 = (GGroup)GetChildAt(13);
            Init();
        }
        partial void Init();
    }
}