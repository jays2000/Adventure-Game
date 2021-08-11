using System;
namespace StarterGame
{
    public class breakCommand : Command // used to break the ice in trap room
    {
        public breakCommand()
        {
            this.Name = "break";
        }

        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.breakIce(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nBreak What?");
            }
            return false;
        }
    }
}
