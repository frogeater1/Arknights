using UnityEngine;

namespace Arknights.Buffs
{
    public class Buff : MonoBehaviour
    {
        public Data.Buff loadData;

        public Unit unit;
        public int level;
        public float duration;

        public int logicFrame;
#if UNITY_EDITOR
        public virtual void Load(Data.Buff data)
        {
            loadData = data;
        }
#endif
        public virtual void Action(Unit u, int lv)
        {
            unit = u;
            logicFrame = 0;
            level = lv;
            duration = loadData.durantion[level - 1];
        }

        public virtual void LogicUpdate()
        {
            logicFrame++;
            if (logicFrame >= Settings.FPS * duration)
            {
                Remove();
            }
        }

        public virtual void Remove()
        {
            unit.buffs.Remove(this);
            Game.Instance.PoolManager.buffs.pools[loadData.id].Release(this);
        }
        
    }
}