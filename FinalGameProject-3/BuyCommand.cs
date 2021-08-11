using System;
namespace StarterGame
{
    public class BuyCommand : Command //command used to buy items in trading room
    {
        public BuyCommand()
        {
            this.Name = "buy";
        }

        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Buy(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nDrop what?");
            }
            return false;
        }
    }
}
