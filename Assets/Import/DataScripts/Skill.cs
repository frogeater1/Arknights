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
        
        public string buff_id;
        
        public string icon_name;
        
        public string effect_name;
        
        public bool permanent;
        
        public bool auto;
        
        public SkillAutoTiming auto_time;
        
        public SkillRangeType range_type;
        
        public string range_id;
        
        public ValueType ext_value_type_1;
        
        public float[] ext_values_1;
        
        public DamageType damage_type;
        
        public int[] start_e;
        
        public int[] cost_e;
        
        public float[] duration;

        public void CopyTo(Skill target)
        {
            target.id = id;
            target.name = name;
            target.script = script;
            target.buff_id = buff_id;
            target.icon_name = icon_name;
            target.effect_name = effect_name;
            target.permanent = permanent;
            target.auto = auto;
            target.auto_time = auto_time;
            target.range_type = range_type;
            target.range_id = range_id;
            target.ext_value_type_1 = ext_value_type_1;
            target.ext_values_1 = ext_values_1;
            target.damage_type = damage_type;
            target.start_e = start_e;
            target.cost_e = cost_e;
            target.duration = duration;
        }
    }
}
