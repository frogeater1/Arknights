using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arknights.Data
{
    [Serializable]
    public partial class Skill
    {
        
        public string id;
        
        public string name;
        
        public string script;
        
        public string[] buff_ids;
        
        public string icon_name;
        
        public bool permanent;
        
        public bool auto;
        
        public SkillAutoTiming auto_time;
        
        public SkillRangeType range_type;
        
        public string range_id;
        
        public int ext_value_1;
        
        public int[] start_e;
        
        public int[] cost_e;

        public void CopyTo(Skill target)
        {
            target.id = id;
            target.name = name;
            target.script = script;
            target.buff_ids = buff_ids;
            target.icon_name = icon_name;
            target.permanent = permanent;
            target.auto = auto;
            target.auto_time = auto_time;
            target.range_type = range_type;
            target.range_id = range_id;
            target.ext_value_1 = ext_value_1;
            target.start_e = start_e;
            target.cost_e = cost_e;
        }
    }
}
