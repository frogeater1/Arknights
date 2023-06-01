using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arknights.Data
{
    [Serializable]
    public partial class SkillRange
    {
        
        public string id;
        
        public int[][] range_array;

        public void CopyTo(SkillRange target)
        {
            target.id = id;
            target.range_array = range_array;
        }
    }
}
