/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_ProgressBar1 : GProgressBar
    {
        public GImage m_n0;
        public GImage m_bar;
        public const string URL = "ui://wnbsox0hoeh33f";

        public static UI_ProgressBar1 CreateInstance()
        {
            return (UI_ProgressBar1)UIPackage.CreateObject("Arknights", "ProgressBar1");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_n0 = (GImage)GetChildAt(0);
            m_bar = (GImage)GetChildAt(1);
            Init();
        }
        partial void Init();
    }
}