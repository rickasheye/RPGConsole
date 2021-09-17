using RPGConsole.Graphical;
using System;
using System.IO;

namespace RPGConsole.InventoryBlock
{
    public class Furnace : Block
    {
        public Furnace() : base("Furnace", 0, 0, 10, Color.GRAY, "Textures/blocks/furnace.png", 1) { }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
        }

        public override void PlayerOnTop(Player player)
        {
            base.PlayerOnTop(player);
            //automatically ask the player
            player.furnace = true;
            Program.unit.AddConsoleItem(new ConsoleItem(3, "It appears you have stepped on a furnace. use the command 'furnace' to activate it!"));
        }

        public override void PlayerOffBlock(Player player)
        {
            base.PlayerOffBlock(player);
            player.furnace = false;
        }
    }
}
