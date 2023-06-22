using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arknights.Data
{
    [Serializable]
    public partial class Buff
    {
        
        public string id;
        
        public string name;
        
        public string script;
        
        public string change_attribute;
        
        public float fixed_value;
        
        public float percentage_value;
        
        public float[] durantion;

        public void CopyTo(Buff target)
        {
            target.id = id;
            target.name = name;
            target.script = script;
            target.change_attribute = change_attribute;
            target.fixed_value = fixed_value;
            target.percentage_value = percentage_value;
            target.durantion = durantion;
        }
    }
}
