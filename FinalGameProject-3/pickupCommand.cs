using System;
namespace StarterGame
{
    public class pickupCommand : Command
    {
        public pickupCommand()
        {
            this.Name = "pickup";
        }

        public override bool Execute(Player player)
        {
            if(this.HasSecondWord())
            {
                player.pickup(SecondWord);
            }
            else
            {
                Console.WriteLine("pickup what?");
            }

            return false;
        }
    }
}
