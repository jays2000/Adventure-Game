using System;
namespace StarterGame
{
    public class StatsCommand : Command
    {
        public StatsCommand()
        {
            this.Name = "stats";
        }

        public override bool Execute(Player player)
        {
            player.Stats();
            return false;
        }
    }
}
