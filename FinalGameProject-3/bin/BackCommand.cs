using System;
using System.Collections.Generic;

namespace StarterGame
{
    public class BackCommand : Command
    {
        public Stack<String> stack = new Stack<String>();

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
