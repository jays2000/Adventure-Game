using System;
namespace StarterGame
{
    public class GiveCommand : Command
    {
        public GiveCommand()
        {
            this.Name = "give";
        }

        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Give(SecondWord);
            }
            else
            {
                Console.WriteLine("Give What?");
            }
            return false;
        }
    }
}
