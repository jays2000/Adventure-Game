using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Room
    {
        private Dictionary<string, Door> exits;
        private string _tag;
        public Dictionary<string, IItem> items;
        public ItemContainer chest; //new
       
        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }
        public Room() : this("No Tag")
        {

        }

        public Room(string tag)
        {
            exits = new Dictionary<string, Door>();
            items = new Dictionary<string, IItem>();
            //chest = new ItemContainer("chest");
            this.Tag = tag;

           
        }

        public void SetExit(string exitName, Door door)
        {
            exits[exitName] = door;
        }

        public Door GetExit(string exitName) // get exit for current room
        {
            Door door = null;
            exits.TryGetValue(exitName, out door);
            return door;
        }

        public string GetExits() //get exits for the current room
        {
            string exitNames = "Exits: ";
            Dictionary<string, Door>.KeyCollection keys = exits.Keys;
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName;
            }

            return exitNames;
        }

        public string Description()
        {
            return "You are in the " + this.Tag + ".\n *** " + this.GetExits();
        }

        public void addItem(ItemContainer itemContainer) //add item to room 
        {
            //items.Add(item.name, item);
            chest = itemContainer;
        }

        public string displayItems() // display items in room 
        {
            string returnString = "";
            foreach (string item in items.Keys)
            {
                returnString += item + ", ";
            }

            return chest.name;
        }

        public void removeItem(String item) //remove item from chest
        {
            items.Remove(item);
        }

        public IItem getItem(string item) // gets item from chest
        {
            if (items.ContainsKey(item))
            {
                items.TryGetValue(item, out IItem I);
                return I;
                
            }
            else
            {
                return null;
            }

        }
        

    }
}
