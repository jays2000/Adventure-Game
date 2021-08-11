using System;
namespace StarterGame
{
    public class DropCommand : Command
    {
        public DropCommand()
        {
            this.Name = "drop";
        }

        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.drop(SecondWord);
            }
            else
            {
                Console.WriteLine("Drop What?");
            }

            return false;
        }
    }
}
