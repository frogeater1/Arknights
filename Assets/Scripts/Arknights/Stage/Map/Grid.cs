using System.Collections.Generic;
using UnityEngine;

namespace Arknights
{
    public class Grid
    {
        public GridType type;
        public Vector2Int logicPos;

        public List<Unit> units;

        public Grid(GridType type, Vector2Int logicPos)
        {
            this.type = type;
            this.logicPos = logicPos;
        }
    }
}