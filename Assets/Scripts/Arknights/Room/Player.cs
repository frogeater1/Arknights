using System.Collections.Generic;

namespace Arknights
{
    public class Player
    {
        public Team team;
        public string name;
        public List<int> selectCardIdxs = new();
    }
}