using System;
namespace StarterGame
{
    public class inspectCommand : Command // inspect the chest in the room
    {
        public inspectCommand()
        {
            this.Name = "inspect";
        }

        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.inspect(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nInspect what?");
            }
            return false;
        }
    }
}
