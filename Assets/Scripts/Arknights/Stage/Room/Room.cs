namespace Arknights
{
    public class Room
    {
        private string name;
        private int Idx;
        private Player player1;
        private Player player2;

        public Room(string RoomName)
        {
            name = RoomName;
        }

        public void  Create()
        {
            player1 = new Player();
        }

        public void Join()
        {
            player2 = new Player();
        }
    }
}