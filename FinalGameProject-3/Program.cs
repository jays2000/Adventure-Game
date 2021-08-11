using System;

namespace StarterGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Game game = new Game();
            game.Start();
            game.Play();
            game.End();
        }
    }
}
