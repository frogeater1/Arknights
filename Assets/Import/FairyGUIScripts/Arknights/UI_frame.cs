/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_frame : GComponent
    {
        public GGraph m_bg;
        public UI_Button_close m_closeButton;
        public const string URL = "ui://wnbsox0hhq874u";

        public static UI_frame CreateInstance()
        {
            return (UI_frame)UIPackage.CreateObject("Arknights", "frame");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GGraph)GetChildAt(0);
            m_closeButton = (UI_Button_close)GetChildAt(1);
            Init();
        }
        partial void Init();
    }
}