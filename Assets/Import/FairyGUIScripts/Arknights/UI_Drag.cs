/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arknights
{
    public partial class UI_Drag : GComponent
    {
        public const string URL = "ui://wnbsox0hozhr49";

        public static UI_Drag CreateInstance()
        {
            return (UI_Drag)UIPackage.CreateObject("Arknights", "Drag");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Init();
        }
        partial void Init();
    }
}