using System.Collections.Generic;

namespace Arknights
{
    public class Player
    {
        public int playerId;
        public string name;
        public Team team;
        public List<int> selectCardIdxs = new();
    }
}