using System;
using UnityEngine;

namespace Arknights.Skills
{
    public class 主动 : Skill
    {
        public int curE;
        public int level;

        private float logicFrame;

        public override void Use(Unit target = null)
        {
        }

        private void OnEnable()
        {
            EventManager.LogicUpdate += LogicUpdate;
        }

        private void OnDisable()
        {
            EventManager.LogicUpdate -= LogicUpdate;
        }

        public override void Init()
        {
            curE = loadData.start_e[level - 1];
            logicFrame = 0;
        }

        private void LogicUpdate()
        {
            var cost_E = loadData.cost_e[level - 1];
            if (curE < cost_E)
            {
                //每60逻辑帧增加1
                logicFrame ++ ;
                if (logicFrame >= 60)
                {
                    curE++;
                    logicFrame = 0;
                }
            }
        }
    }
}