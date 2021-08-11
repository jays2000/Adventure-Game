using System;
using System.Collections.Generic;

namespace StarterGame
{
    public class BackCommand : Command // used to go back to last room
    {
        

        public BackCommand()
        {
            this.Name = "back";
        }

        public override bool Execute(Player player)
        {
            player.back();
            return false;
        }
    }
}
