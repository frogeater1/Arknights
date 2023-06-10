using System;
using UnityEngine;

namespace Arknights.Skills
{
    public class 主动 : Skill
    {
        public int curE;
        public int level;

        private float time;
        public override void Use()
        {
            
        }

        public override void Init()
        {
            curE = loadData.start_e[level - 1];
            time = 0;
        }

        private void Update()
        {
            var cost_E = loadData.cost_e[level - 1];
            if (curE < cost_E)
            {
               //每秒增加1
               time += Time.deltaTime;
               if (time >= 1f)
               {
                   curE++;
                   time = 0;
               }
            }
        }
    }
}