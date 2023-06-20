/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_MainMenu_1 : GComponent
    {
        public Controller m_c1;
        public UI_Button_white m_Terminal;
        public UI_Button_white m_Team;
        public UI_Button_blue m_n18;
        public UI_Button_white m_n21;
        public UI_Button_white m_n22;
        public UI_Button_white m_n23;
        public GGraph m_n24;
        public UI_Button_blue m_n25;
        public UI_Button_blue m_n26;
        public GGraph m_n27;
        public GTextField m_n36;
        public GGraph m_n37;
        public UI_Button_add m_n48;
        public GImage m_n51;
        public GImage m_n52;
        public GImage m_n53;
        public GTextField m_n54;
        public GTextField m_n55;
        public GTextField m_n56;
        public GImage m_n58;
        public UI_Button_空白 m_n59;
        public GTextField m_n60;
        public GTextField m_n61;
        public GTextField m_n62;
        public GTextField m_n63;
        public GTextField m_n64;
        public GImage m_n77;
        public GImage m_n65;
        public GGraph m_n69;
        public GTextField m_n70;
        public GTextField m_n71;
        public GTextField m_n72;
        public GTextField m_n73;
        public GGraph m_n76;
        public GImage m_n78;
        public GImage m_n79;
        public GTextField m_n80;
        public GImage m_n81;
        public GGraph m_n83;
        public GImage m_n84;
        public GTextField m_n85;
        public GImage m_n86;
        public GImage m_n87;
        public GImage m_n88;
        public GImage m_n89;
        public UI_Button_gray m_n99;
        public GImage m_n90;
        public GTextField m_n91;
        public GTextField m_n92;
        public GTextField m_n93;
        public GTextField m_n94;
        public GTextField m_n95;
        public GTextField m_n96;
        public GGraph m_n97;
        public GGraph m_n98;
        public UI_Battery m_n100;
        public const string URL = "ui://wnbsox0ha0xza";

        public static UI_MainMenu_1 CreateInstance()
        {
            return (UI_MainMenu_1)UIPackage.CreateObject("Arknights", "MainMenu_1");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m_Terminal = (UI_Button_white)GetChildAt(0);
            m_Team = (UI_Button_white)GetChildAt(1);
            m_n18 = (UI_Button_blue)GetChildAt(2);
            m_n21 = (UI_Button_white)GetChildAt(3);
            m_n22 = (UI_Button_white)GetChildAt(4);
            m_n23 = (UI_Button_white)GetChildAt(5);
            m_n24 = (GGraph)GetChildAt(6);
            m_n25 = (UI_Button_blue)GetChildAt(7);
            m_n26 = (UI_Button_blue)GetChildAt(8);
            m_n27 = (GGraph)GetChildAt(9);
            m_n36 = (GTextField)GetChildAt(10);
            m_n37 = (GGraph)GetChildAt(11);
            m_n48 = (UI_Button_add)GetChildAt(12);
            m_n51 = (GImage)GetChildAt(13);
            m_n52 = (GImage)GetChildAt(14);
            m_n53 = (GImage)GetChildAt(15);
            m_n54 = (GTextField)GetChildAt(16);
            m_n55 = (GTextField)GetChildAt(17);
            m_n56 = (GTextField)GetChildAt(18);
            m_n58 = (GImage)GetChildAt(19);
            m_n59 = (UI_Button_空白)GetChildAt(20);
            m_n60 = (GTextField)GetChildAt(21);
            m_n61 = (GTextField)GetChildAt(22);
            m_n62 = (GTextField)GetChildAt(23);
            m_n63 = (GTextField)GetChildAt(24);
            m_n64 = (GTextField)GetChildAt(25);
            m_n77 = (GImage)GetChildAt(26);
            m_n65 = (GImage)GetChildAt(27);
            m_n69 = (GGraph)GetChildAt(28);
            m_n70 = (GTextField)GetChildAt(29);
            m_n71 = (GTextField)GetChildAt(30);
            m_n72 = (GTextField)GetChildAt(31);
            m_n73 = (GTextField)GetChildAt(32);
            m_n76 = (GGraph)GetChildAt(33);
            m_n78 = (GImage)GetChildAt(34);
            m_n79 = (GImage)GetChildAt(35);
            m_n80 = (GTextField)GetChildAt(36);
            m_n81 = (GImage)GetChildAt(37);
            m_n83 = (GGraph)GetChildAt(38);
            m_n84 = (GImage)GetChildAt(39);
            m_n85 = (GTextField)GetChildAt(40);
            m_n86 = (GImage)GetChildAt(41);
            m_n87 = (GImage)GetChildAt(42);
            m_n88 = (GImage)GetChildAt(43);
            m_n89 = (GImage)GetChildAt(44);
            m_n99 = (UI_Button_gray)GetChildAt(45);
            m_n90 = (GImage)GetChildAt(46);
            m_n91 = (GTextField)GetChildAt(47);
            m_n92 = (GTextField)GetChildAt(48);
            m_n93 = (GTextField)GetChildAt(49);
            m_n94 = (GTextField)GetChildAt(50);
            m_n95 = (GTextField)GetChildAt(51);
            m_n96 = (GTextField)GetChildAt(52);
            m_n97 = (GGraph)GetChildAt(53);
            m_n98 = (GGraph)GetChildAt(54);
            m_n100 = (UI_Battery)GetChildAt(55);
            Init();
        }
        partial void Init();
    }
}