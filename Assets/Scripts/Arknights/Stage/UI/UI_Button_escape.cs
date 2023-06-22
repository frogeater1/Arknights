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
                Commander.Exit(Game.Instance.CharacterManager.curCharacter);
                Game.Instance.CharacterManager.curCharacter = null;
            });
        }
    }
}