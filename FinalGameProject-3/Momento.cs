/**
 * Momento class used to contain state of object
 * 
 */
using System;
namespace StarterGame
{
    public class Momento
    {


        private Room _room;
        private bool containFlower = false;
        public Momento(Room room)
        {
            _room = room;
        }

        public Room getState()
        {
            return _room;
        }
    }
}
