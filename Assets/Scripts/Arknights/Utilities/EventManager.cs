using System;

namespace Arknights
{
    public static class EventManager
    {
        public static event Action<方向> ChangeDirection;

        public static void CallChangeDirection(方向 direction)
        {
            ChangeDirection?.Invoke(direction);
        }
        
        public static event Action CancelSelect;
        
        public static void CallCancelSelect()
        {
            CancelSelect?.Invoke();
        }
    }
}