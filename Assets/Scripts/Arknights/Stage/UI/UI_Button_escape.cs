using UnityEngine;

namespace Arknights
{
    public partial class UI_Button_escape
    {
        partial void Init()
        {
            onClick.Add(() =>
            {
                Game.Instance.CharacterManager.curCharacter.回收();
                EventManager.CallCancelSelect();
            });
        }
    }
}