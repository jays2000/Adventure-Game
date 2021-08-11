using System;
namespace StarterGame
{
    public class unlockCommand : Command
    {
        public unlockCommand()
        {
            this.Name = "unlock";
        }

        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.unlock(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nGo Where?");
            }
            return false;
        }
    }
}
