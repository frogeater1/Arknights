using System;

namespace Arknights
{
    public static class EventManager
    {
        public static event Action LogicUpdate;
        
        public static void CallLogicUpdate()
        {
            LogicUpdate?.Invoke();
        }
        
        //注意静态事件仅用于处理View层面，不要用于处理Model层面，Model层面的事件应该直接从Game的索引中调用
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