using UnityEngine;

namespace Arknights
{
    partial class UI_Button_取消
    {
        partial void Init()
        {
            onClick.Add(Click);
        }
        
        private void Click()
        {
            EventManager.CallCancelSelect();
            Game.Instance.CharacterManager.curCharacter.Hide();
            Game.Instance.ui_battle.CancelSelect();
            Game.Instance.CharacterManager.curCharacter = null;
        }
    }
}