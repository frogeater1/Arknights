using System;

namespace Arknights
{
    public static class EventManager
    {
        public static event Action<Character, 方向> ChangeDirection;

        public static void CallChangeDirection(Character character, 方向 direction)
        {
            ChangeDirection?.Invoke(character, direction);
        }

        public static event Action CancelSelect;

        public static void CallCancelSelect()
        {
            CancelSelect?.Invoke();
        }
    }
}