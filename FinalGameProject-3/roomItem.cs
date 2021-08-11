using System;
using System.Collections.Generic;
namespace StarterGame
{

    public interface IItem
    {
        String name { get; set; }
        float weight { get; set; }
        String Description { get; }
        void addDecorator(IItem decorator);
        int value { get; set; }
        bool isContainer { get; }
        void AddItem(IItem item);
        IItem RemoveItem(String itemName);
    }

    public class roomItem : IItem
    {

        public String name { get; set; }
        public float weight { get; set; }
        private String _description { get; set; }
        public bool grabbable { get; set; }
        public String Description { get { return _description; } }
        private IItem _decorator;
        public bool isContainer { get { return false; } }
        private int _value;
        public int value
        {
            get { return _value; }
            set { _value = value; }
        }

        public roomItem() : this("Nameless") { }
        public roomItem(String name) : this(name, 1f) { }
        public roomItem(String name, float weight) : this(name, weight, true) { }

        public roomItem(string name, float weight, bool grab) : this(name, weight, grab, 0) { }

        public roomItem(string name, float weight,bool grab, int value)
        {
            this.name = name;
            this.weight = weight;
            grabbable = grab;
            this.value = value;
            _description = "Item Description: name: " + name + "," + "weight: " + weight + "," + "value: " + value;
        }



            
        

        public void addDecorator(IItem decorator)
        {
            if(_decorator == null)
            {
                _decorator = decorator;

            }
            else
            {
                _decorator.addDecorator(decorator);
            }
        }

        public void AddItem(IItem item)
        {

        }
        public IItem RemoveItem(String itemName)
        {
            return null;
        }


    }

    public class ItemContainer : IItem // hierarchy design pattern
    {
        private Dictionary<String, IItem> _chest;
        public String name { get; set; }
        private float _weight;
        public float weight
        {
            get
            {
                float containedWeight = 0;
                foreach (IItem item in _chest.Values) // add all weights together 
                {
                    containedWeight += item.weight;
                }
                return _weight + containedWeight;
            }
            set { _weight = value; }
        }
        private String _description { get; set; }
        public bool grabbable { get; set; }
        public String Description {
            get {
                if (_isLocked)
                {
                    return "Chest is locked";
                }
                else
                {
                    string itemList = "Items: ";
                    foreach (IItem item in _chest.Values)
                    {
                        itemList += "\n " + item.Description;
                    }
                    return "Name: " + name + ", Weight: " + weight + "," + _description + "\n" + itemList;
                }
            }

        }
        private IItem _decorator;
        public bool isContainer { get { return true; } }
        private int _value;
        public int value
        {
            get { return _value; }
            set { _value = value; }
        }
        private bool _isLocked;
        public bool isLocked
        {
           get{ return _isLocked; }
            set { _isLocked = value; }

        }

        public ItemContainer() : this("Chest") { }
        public ItemContainer(String name) : this(name, 1f) { }
        public ItemContainer(String name, float weight) : this(name, weight, true) { }

        public ItemContainer(string name, float weight, bool grab) : this(name, weight, grab, 0) { }

        public ItemContainer(string name, float weight, bool grab, int value)
        {
            this.name = name;
            _chest = new Dictionary<string, IItem>();
            this.weight = weight;
            grabbable = grab;
            this.value = value;
            _description = "Item Description: name: " + name + "," + "weight: " + weight + "," + "value: " + value;
        }

        public void addDecorator(IItem decorator) //decorate object
        {
            if (_decorator == null)
            {
                _decorator = decorator;

            }
            else
            {
                _decorator.addDecorator(decorator);
            }
        }

        public void AddItem(IItem item) // add item in container object
        {
            _chest[item.name] = item;
        }
        public IItem RemoveItem(String itemName) //remove item from container object
        {
            IItem item = null;
            _chest.Remove(itemName, out item);
            return item;
        }

        public void Lock() //locks chest
        {
            _isLocked = true;

        }

        public void unlock() //unlocks chest
        {
            _isLocked = false;
        }

            
    }
}
