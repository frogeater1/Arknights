using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arknights.Data
{
    [Serializable]
    public partial class Grid
    {
        
        public string id;
        
        public int stage;
        
        public GridType[] gridline;

        public void CopyTo(Grid target)
        {
            target.id = id;
            target.stage = stage;
            target.gridline = gridline;
        }
    }
}
