using System;
namespace StarterGame
{
    public class Monster
    {
        private static Monster _instance;
        private bool _containsFlame = false;
        public bool containFlame
        {
            get { return _containsFlame; }
            set { _containsFlame = value; }
        }
     
        public static Monster Instance  //singleton pattern
        {
            get
            {

                if (_instance == null) // if no instance create gamworld instance
                {
                    _instance = new Monster(); // create gameworld
                }
                return _instance;
            }
        }

        public void speak()
        {
            Console.WriteLine("Monster: Bring me the sacred blue flame in exchange for the key!");
        }


    }
}
