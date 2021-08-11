using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Player
    {
        private Room _currentRoom = null;
        private float maxWeight = 50;
        private int playerValue = 0;
        private CareTaker careTaker;
        private Dictionary<string, IItem> inventory;
        public Dictionary<string, IItem> Inventory
        {
            get { return inventory; }
        }
   
        public Room CurrentRoom
        {
            get
            {
                return _currentRoom;
            }
            set
            {
                _currentRoom = value;
            }
        }


        public Player(Room room)
        {
            _currentRoom = room;
            inventory = new Dictionary<string, IItem>();
            careTaker = new CareTaker();
            careTaker.add(new Momento(null));


        }

        public void WalkTo(string direction) //Player walks to room
        {

            Door door = _currentRoom.GetExit(direction); //gets exit direction door 

            if (door != null)
            {
                if (door.isLocked)
                {
                    Console.WriteLine("Door is locked");
                }
                else
                {
                    careTaker.add(saveStateToMomento()); // add current room into caretaker class stack
                    Notification playerLeavingRoom = new Notification("PlayerLeavingRoom", this);
                    NotificationCenter.Instance.PostNotification(playerLeavingRoom); // post notification to notification center
                    Room nextRoom = door.getOtherSideRoom(CurrentRoom);  // assign other side of door to next room
                    CurrentRoom = door.getOtherSideRoom(CurrentRoom);
                    this._currentRoom = nextRoom;
                    Notification playerEnteredRoom = new Notification("PlayerEnteredRoom", this);
                    NotificationCenter.Instance.PostNotification(playerEnteredRoom);
                    this.OutputMessage("\n" + this._currentRoom.Description());
                    Console.WriteLine("Items in room: " + _currentRoom.displayItems());
                }


            }
            else
            {
                this.OutputMessage("\nThere is no door on " + direction);
            }
        }

        public Momento saveStateToMomento() //creates new momento and saves current state/room
        {
            return new Momento(CurrentRoom);
        }

        public void getStateFromMomento(Momento momento) //gets last "state"/room that was saved 
        {
            try
            {
                CurrentRoom = momento.getState();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("No previous room");
                
            }


        }

        public void back() // back command 
        {
            getStateFromMomento(careTaker.Get());//get last state that was saved and assign to current room
            Room room = CurrentRoom;
            if (room != null)
            {
                _currentRoom = room;
                Notification notification = new Notification("PlayerWentBack", this);
                NotificationCenter.Instance.PostNotification(notification);
                this.OutputMessage("\n" + this._currentRoom.Description());
                Console.WriteLine("Items in room: " + _currentRoom.displayItems());
            }
            else
            {
                Console.WriteLine("No previous room");


            }
        }

        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }


        public void pickup(string item) //player picksup item
        {
            IItem a = null;
            IItem i2 = CurrentRoom.chest.RemoveItem(item);
            if (i2 != null)
            {
                if (maxWeight - i2.weight > 0) //checks if item can be picked up and is less than weight
                {
                    Notification notification = new Notification("PlayerPickedUpItem", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    inventory.Add(i2.name, i2);
                    maxWeight = maxWeight - i2.weight;
                    CurrentRoom.chest.RemoveItem(item);

                }
                else
                {
                    Console.WriteLine("Item exceeds weight capacity");
                }
            } else
            {
                Console.WriteLine("Item not found");
            }
        }


        public void drop(string item) //player drops item
        {
            if (inventory.ContainsKey(item)) //check if item is in inventory 
            {
                IItem i;
                inventory.TryGetValue(item, out i);
                inventory.Remove(item);
                Notification notification = new Notification("PlayerDroppedItem", this);
                NotificationCenter.Instance.PostNotification(notification);
                maxWeight = maxWeight + i.weight; //return weight back to player
                CurrentRoom.chest.AddItem(i);
            }
            else
            {
                Console.WriteLine("Item not in inventory");
            }
        }



        public void breakIce(String item) { //breaks ice in trap room

            if (inventory.ContainsKey("icepick") && item == "ice") {

                //CurrentRoom.removeItem(item);
                Notification notification = new Notification("PlayerBrokeIce", this);
                NotificationCenter.Instance.PostNotification(notification);
            }
        }



        public void unlock(String direction) // tries to unlock door 
        {
            Door door = this._currentRoom.GetExit(direction); //gets exit direction door
            String name = door.getOtherSideRoom(CurrentRoom).Tag;

            if (door.isLocked)
            {
                if (inventory.ContainsKey(name + "Key"))
                {

                    door.isLocked = false;
                    Notification notification = new Notification("PlayerUnlockedDoor", this);
                    NotificationCenter.Instance.PostNotification(notification);
                }
                else
                {
                    Console.WriteLine("Must obtain key to lock door");
                }
            }
            else
            {
                Console.WriteLine("Door is unlocked");
            }

        }


        public void inspect(String item) //inspect item
        {
            IItem I;
            Console.WriteLine(CurrentRoom.chest.Description);

        }

        public void sell(String item) // sell item in trade room
        {
            if (inventory.ContainsKey(item) && CurrentRoom.Tag == "Trade Room")
            {
                IItem i;
                inventory.TryGetValue(item, out i);
                playerValue += i.value; //increase playervalue
                inventory.Remove(item); //remove from inventory
                CurrentRoom.chest.AddItem(i); //add item to room chest
                maxWeight += i.weight; // add weight back to inventory
                Console.WriteLine("You sold " + item + " for " + i.value);
            }
            else
            {
                Console.WriteLine("Item not in inventory");
            }


        }

        public void Buy(string item) //player buys items from 
        {
            if (CurrentRoom.Tag == "Trade Room") // checks if current room
            {
                IItem a = null;
                IItem i2 = CurrentRoom.chest.RemoveItem(item); //remove item from room
                if (i2 != null) 
                {
                    if (maxWeight - i2.weight > 0 && i2.value <= playerValue) //checks if item can be picked up and is less than weight
                    {
                        Notification notification = new Notification("PlayerPickedUpItem", this);
                        NotificationCenter.Instance.PostNotification(notification);
                        inventory.Add(i2.name, i2);
                        maxWeight = maxWeight - i2.weight; //subtract from max weight
                        playerValue += i2.value; //adds value to player

                    }
                    else
                    {
                        CurrentRoom.chest.AddItem(i2); 
                        Console.WriteLine("Item cannot be added to inventory");
                    }
                }
                else
                {
                    Console.WriteLine("Item not found");
                }
            }
            else { Console.WriteLine("Action cannot be performed"); }
        }

        public void Give(String item) // give blueFlame to monster
        {
            if (inventory.ContainsKey(item) && CurrentRoom.Tag == "dungeon")
            {
                Monster.Instance.containFlame = true; //monster has blueFlame
                Notification notification = new Notification("PlayerGaveBlueFlame", this);
                NotificationCenter.Instance.PostNotification(notification);
                

            }
            else
            {
                Console.WriteLine("Item cannot be given");
            }
        }

        public void Stats() //shows player stats
        {
            Console.WriteLine("Player value:" + playerValue + "\n" + "available inventory weight: " + maxWeight);

            String itemList = "Items: ";
            foreach (IItem i in inventory.Values)
            {

                itemList += "\n " + i.Description;

            }

            Console.WriteLine(itemList);


        }
    }

}