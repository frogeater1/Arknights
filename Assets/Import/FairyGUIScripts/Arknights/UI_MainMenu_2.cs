/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_MainMenu_2 : GComponent
    {
        public GList m_n3;
        public const string URL = "ui://wnbsox0ha0xz1w";

        public static UI_MainMenu_2 CreateInstance()
        {
            return (UI_MainMenu_2)UIPackage.CreateObject("Arknights", "MainMenu_2");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_n3 = (GList)GetChildAt(0);
            Init();
        }
        partial void Init();
    }
}