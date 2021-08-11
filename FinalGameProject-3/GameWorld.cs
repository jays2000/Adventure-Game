using System;
using System.Collections.Generic;
namespace StarterGame
{
    public class GameWorld
    {
        private static GameWorld _instance;
        public static GameWorld Instance  //singleton pattern
        {
            get
            {

                if (_instance == null) // if no instance create gamworld instance
                {
                    _instance = new GameWorld(); // create gameworld
                }
                return _instance;
            }
        }
        private int _trapTimeOut; //keeps count of trap time
        public  int TrapTimeOut
        {
            get { return _trapTimeOut; }
            private set { _trapTimeOut = value; }
        }

        
        private Room _entrance;

        public Room Entrance //set entrance
        {
            get
            {
                return _entrance;
            }

            private set { _entrance = value; }
        }

      

        private Room _trap; // trap room
        public Room Trap
        {
            get { return _trap; }
            private set { _trap = value; }
        }

        private Room _storedTrap; // stored trap room
        public Room storedTrap
        {
            get { return _storedTrap; }
            private set { _storedTrap = value; }
        }

        private Room Previous;
        public Room _previousRoom
        {
            get { return Previous; }
            private set { Previous = value; }
        }

        private int _token; //keeps track if trap is off or on
        public int Token
        {
            get { return _token; }
            set { _token = value; }
        }

        private Room _transporter; // transporter room
        public Room Transporter
        {
            get { return _transporter; }
            set { _transporter = value; }
        }

        private Room _tradeRoom; // trade room
        public Room tradeRoom
        {
            get { return _tradeRoom; }
            set { _tradeRoom = value; }
        }
     
        private Room _dungeon; // dungeon room variable
        public Room dungeonRoom
        {
            get { return _dungeon; }
            set { _dungeon = value; }
        }
        
        private List<Room> roomList = new List<Room>(); // room list to get random room


        private GameWorld()
        {
            CreateWorld(); // creates world

            // subscribe to notification
            NotificationCenter.Instance.AddObserver("PlayerEnteredRoom", enterRoom); // subscribe to notification
            NotificationCenter.Instance.AddObserver("PlayerPickedUpItem", pickUpItem);
            NotificationCenter.Instance.AddObserver("PlayerDroppedItem", dropItem);
            NotificationCenter.Instance.AddObserver("PlayerWentBack", back);
            NotificationCenter.Instance.AddObserver("PlayerLeavingRoom", playerLeavingRoom);
            NotificationCenter.Instance.AddObserver("PlayerBrokeIce",playerBrokeIce);
            NotificationCenter.Instance.AddObserver("PlayerUnlockedDoor", unlockDoor);
            NotificationCenter.Instance.AddObserver("PlayerGaveBlueFlame", gaveBlueFlame);

        }

        public void gaveBlueFlame(Notification notification)
        {
            Player player = (Player)notification.Object;
            Monster.Instance.containFlame = true; //monster has blueFlame
            player.CurrentRoom.chest.unlock();
        }

       
        public void playerBrokeIce(Notification notification) //player breaks ice to leave room
        {
            Console.WriteLine("You have broken the ice");
            if(Previous == _trap)
            {
               Trap = null;
               NotificationCenter.Instance.RemoveObserver("GameClockTick", TimedTrap);
               TrapTimeOut = 1;
            }
        }


        public void playerLeavingRoom(Notification notification) //player leaving room notification
        {
            Player player = (Player)notification.Object;
            Previous = player.CurrentRoom;
            if(Previous == _storedTrap && TrapTimeOut > 0 )
            {
                Token = 0;
                NotificationCenter.Instance.RemoveObserver("GameClockTick",TimedTrap); // remove observer form timetrap after player leaves room
             
            }
           
        }

        public void back(Notification notification) // player went back notification 
        {
            Player player = (Player)notification.Object;
            if (player.CurrentRoom == Trap)
            {
                player.CurrentRoom = _storedTrap;
            }
            Console.WriteLine("Player went back to previous room");
        }


        public void dropItem(Notification notification) //drop item notification
        { 

            Console.WriteLine("Player dropped item");

        }
        public void pickUpItem(Notification notification) // pick up notification
        {
            Console.WriteLine("Player picked up item");
            Player player = (Player)notification.Object;
            if (player.Inventory.ContainsKey("crown"))
            {
                Console.WriteLine("Congrats!! you beat the game!");
            }

        }
        public void enterRoom(Notification notification) // enter room notification 
        {
            Player player = (Player)notification.Object;
            if (Previous == Trap) //check if previous room was trap
            {
                player.CurrentRoom = Trap;
                Console.WriteLine("The door is frozen");
                
            }
            if (player.CurrentRoom == _storedTrap) //if player enters room they have 10 sec to leave
            {
                if(Token == 0) { 
                    NotificationCenter.Instance.AddObserver("GameClockTick", TimedTrap); // gives notification to timetrap
                    
              
                    TrapTimeOut = 2;
                    //freezeTime = 3;
                }
            }
            if(player.CurrentRoom == Transporter)
            {
                Console.WriteLine("You have entered the transporter room");
                Random random = new Random();
                int r = random.Next(roomList.Count); 
                player.CurrentRoom = roomList[r]; // select random room from list
            }
            if(player.CurrentRoom == tradeRoom)
            {
                Console.WriteLine("You may buy and sell items here");
            }
            if(player.CurrentRoom == dungeonRoom)
            {
                Monster.Instance.speak();
            }
            


            
            
        }


      
        public void unlockDoor(Notification notification) // unlock door notification
        {
            Player player = (Player)notification.Object;

            Console.WriteLine("Player unlocked Room Door");

            
            
        }

        public void TimedTrap(Notification notification) //sets trap
        {

            TrapTimeOut--;
            
            if (TrapTimeOut <= 0) //set trap if timeout
            {
                Token++;
                Console.WriteLine("Oh no the door has frozen!");
                Trap = _storedTrap;
                NotificationCenter.Instance.RemoveObserver("GameClockTick", TimedTrap);
            }

        }
      
        

        private void CreateWorld() // create rooms , set exits, create items
        {
            Room outside = new Room("outside the main entrance of the castle"); //outside of the castle
            Room theGreatHall = new Room("Great Hall of the castle"); // main area of castle
            Room kitchen = new Room("kitchen");
            Room dungeon = new Room("dungeon");
            Room bathroom = new Room("bathroom");
            Room chapel = new Room("Chapel");
            Room theWard = new Room("ward");
            Room theWardrobe = new Room("wardrobe"); // dressing room
            Room throneRoom = new Room("throneRoom"); // where king and queen throne reside
            Room lordsRoom = new Room("lordsRoom"); // king and queen bedroom
            Room theBottlery = new Room("bottlery"); //where wine was kept
            Room garden = new Room("garden");
            Room iceHouse = new Room("ice house");
            Room gateHouse = new Room("gate house");
            Room undercroft = new Room("undercroft");
            Room TradeRoom = new Room("Trade Room");

            //add all rooms to list for transporter 
            roomList.Add(outside);
            roomList.Add(theGreatHall);
            roomList.Add(kitchen);
            roomList.Add(bathroom);
            roomList.Add(chapel);
            roomList.Add(theWard);
            roomList.Add(theWardrobe);
            roomList.Add(throneRoom);
            roomList.Add(lordsRoom);
            roomList.Add(theBottlery);
            roomList.Add(garden);
            roomList.Add(iceHouse);
            roomList.Add(gateHouse);

          

            //create items
            roomItem key = new roomItem("key", 2, true);
            roomItem bible = new roomItem("bible", 2, false,8);
            roomItem wine = new roomItem("wine", 3, true,10);
            roomItem cheese = new roomItem("cheese", 1, true,2);
            //roomItem throne = new roomItem("throne", 100, true);
            roomItem crown = new roomItem("crown", 10, true);
            roomItem bread = new roomItem("bread", 2, true,3);
            roomItem flowers = new roomItem("flower", 1, true);
            roomItem icePick = new roomItem("icepick", 5, true,5);
            //roomItem ice = new roomItem("ice", 1, false);
            roomItem dungeonKey = new roomItem("dungeonKey", 1, true);
            roomItem blueFlame = new roomItem("blueFlame", 30, false, 50);
            roomItem sword = new roomItem("sword", 10.5f, true,15);
            roomItem armour = new roomItem("armour", 12, true,20);
            roomItem jewelry = new roomItem("jewelry", 20, true, 20);
            IItem shield = new roomItem("shield", 5, true, 10);
            roomItem diamonds = new roomItem("diamonds", 5, true, 30);
            roomItem lordsKey = new roomItem("lordsRoomKey", 2, true);
            


            //create chests and add items in chest
            ItemContainer chest = new ItemContainer("chest");
            chest.AddItem(shield);
            chest.AddItem(blueFlame);
            TradeRoom.addItem(chest);

            chest = new ItemContainer("chest");
            theGreatHall.addItem(chest);

            chest = new ItemContainer("chest");
            outside.addItem(chest);

            chest = new ItemContainer("chest");
            theWard.addItem(chest);

            chest = new ItemContainer("chest");
            garden.addItem(chest);

            chest = new ItemContainer("chest");
            chest.AddItem(crown);
            lordsRoom.addItem(chest);

            chest = new ItemContainer("chest");
            chest.AddItem(icePick);
            iceHouse.addItem(chest);


            chest = new ItemContainer("chest");
            chest.AddItem(dungeonKey);
            theWardrobe.addItem(chest);

            chest = new ItemContainer("chest");
            chest.AddItem(sword);
            chest.AddItem(armour);
            gateHouse.addItem(chest);

            chest = new ItemContainer("chest");
            chest.AddItem(wine);
            theBottlery.addItem(chest);

            chest = new ItemContainer("chest");
            chest.AddItem(cheese);
            chest.AddItem(bread);
            kitchen.addItem(chest);
            
            chest = new ItemContainer("chest");
            chest.AddItem(bible);
            chapel.addItem(chest);

            chest = new ItemContainer("chest");
            chest.AddItem(lordsKey);
            chest.Lock();
            dungeon.addItem(chest);

            chest = new ItemContainer("chest");
            throneRoom.addItem(chest);

            chest = new ItemContainer("chest");
            chest.AddItem(diamonds);
            bathroom.addItem(chest);

            

            // create doors and sets exits for each room

            
            Door door = Door.CreateDoor(outside, theGreatHall, "north", "south",false);

            door = Door.CreateDoor(outside, gateHouse, "east", "west",false);

            door = Door.CreateDoor(outside, garden, "west", "east",false);

            door = Door.CreateDoor(garden, iceHouse, "west", "east",false);

            door = Door.CreateDoor(theGreatHall, kitchen, "east", "west",false);

            door = Door.CreateDoor(theGreatHall, chapel, "west", "east",false);

            door = Door.CreateDoor(theGreatHall, theWard, "north", "south",false);

            door = Door.CreateDoor(theGreatHall, kitchen, "east", "west",false);

            door = Door.CreateDoor(kitchen, theBottlery, "north", "south",false);

            door = Door.CreateDoor(undercroft, theBottlery, "east", "west",false);

            door = Door.CreateDoor(dungeon, theBottlery, "north", "east",true);

            door = Door.CreateDoor(theWardrobe, bathroom, "west", "east",false);

            door = Door.CreateDoor(theWardrobe, lordsRoom, "east", "west",true);

            door = Door.CreateDoor(chapel, throneRoom, "north", "south",false);

            // door = Door.CreateDoor(theWard, lordsRoom, "north", "south",true);
            door = Door.CreateDoor(theWard, theWardrobe, "north", "south", false);

            door = Door.CreateDoor(garden, TradeRoom, "north", "south",false);

            Entrance = outside;
            _storedTrap = iceHouse;
            _trap = null;
            Token = 0; //takes count of if the trap is still going
            Transporter = undercroft; //transporter room
            tradeRoom = TradeRoom; 
            dungeonRoom = dungeon;
           
        }

    }

}
