using System;
namespace StarterGame
{
    public class SellCommand : Command // Command used to sell items in trading room
    {
        public SellCommand()
        {
            this.Name = "sell";
        }

        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.sell(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nSell what?");
            }
            return false;
        }
    }
}
