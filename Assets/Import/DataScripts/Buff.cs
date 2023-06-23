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
        
        public string prefab;
        
        public string[] change_attributes;
        
        public ValueType[] value_types;
        
        public float[] value;
        
        public float[] durantion;

        public void CopyTo(Buff target)
        {
            target.id = id;
            target.name = name;
            target.prefab = prefab;
            target.change_attributes = change_attributes;
            target.value_types = value_types;
            target.value = value;
            target.durantion = durantion;
        }
    }
}
