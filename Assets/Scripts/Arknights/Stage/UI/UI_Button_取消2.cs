namespace Arknights
{
    public partial class UI_Button_取消2
    {
        partial void Init()
        {
            onClick.Add(Click);
        }
        
        private void Click()
        {
            EventManager.CallCancelSelect();
            Game.Instance.ui_battle.CancelSelect();
            Game.Instance.CharacterManager.curCharacter = null;
        }
    }
}
