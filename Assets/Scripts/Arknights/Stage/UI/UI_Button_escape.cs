using UnityEngine;

namespace Arknights
{
    public partial class UI_Button_escape
    {
        partial void Init()
        {
            onClick.Add(() =>
            {
                EventManager.CallCancelSelect();
                Game.Instance.CharacterManager.curCharacter.回收();
                Game.Instance.ui_battle.回收();
                Game.Instance.CharacterManager.curCharacter = null;
            });
        }
    }
}