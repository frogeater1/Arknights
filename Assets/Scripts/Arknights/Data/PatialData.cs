// 写一些Database类的扩展

using System.Collections.Generic;
using UnityEngine;

namespace Arknights.Data
{
    public partial class SkillRange
    {
        public List<Vector2Int> Range
        {
            get
            {
                List<Vector2Int> range = new ();
                
                for (int i = 0; i < range_array.Length; i++)
                {
                    if (range_array[i].Length > 1)
                        range.Add(new Vector2Int(range_array[i][0], range_array[i][1]));
                }
                return range;
            }
        }
    }
}