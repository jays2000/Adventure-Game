using System;
namespace StarterGame
{
    public class Door
    {
        private Room room1;
        private Room room2;
        
        public Door(Room room1, Room room2, bool isLocked)
        {
            this.room1 = room1;
            this.room2 = room2;
            this.isLocked = isLocked;
        }

        //check if door is locked
        private  bool _islocked;
        public bool isLocked
        {
            get { return _islocked; }
            set { _islocked = value; }
        }

        // assign other side of room
        public Room getOtherSideRoom(Room room)
        {
            if (room == room1)
            {
                return room2;
            }
            else
            {
                return room1;
            }

        }

        //create doors and set exits
        public static Door CreateDoor(Room room1, Room room2, String direction1 ,String direction2, bool isLocked)
        {
            Door door = new Door(room1, room2,isLocked);
            room1.SetExit(direction1,door);
            room2.SetExit(direction2, door);
            
            return door;


        }
    }
}
