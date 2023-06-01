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
        
        public bool auto;
        
        public SkillAutoTiming auto_time;
        
        public SkillRangeType range_type;
        
        public string range_id;
        
        public string change_attribute;
        
        public float fixed_value;
        
        public float percentage_value;
        
        public int ext_value_1;

        public void CopyTo(Skill target)
        {
            target.id = id;
            target.name = name;
            target.script = script;
            target.auto = auto;
            target.auto_time = auto_time;
            target.range_type = range_type;
            target.range_id = range_id;
            target.change_attribute = change_attribute;
            target.fixed_value = fixed_value;
            target.percentage_value = percentage_value;
            target.ext_value_1 = ext_value_1;
        }
    }
}
