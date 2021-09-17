using RPGConsole.Graphical;
using System;
using System.IO;

namespace RPGConsole.InventoryBlock
{
    public class CraftingTable : Block
    {
        public CraftingTable() : base("CT", 0, 0, 1, Color.BROWN, "Textures/blocks/crafting.png", 1) { 

            giveBlock = true; 
        }

        public override void PlayerOnTop(Player player)
        {
            base.PlayerOnTop(player);
            //find the player if it is then enable crafting bench.
            player.craftingTable = true;
        }

        public override void PlayerOffBlock(Player player)
        {
            base.PlayerOffBlock();
            //find when the player is off the block
            player.craftingTable = false;
        }
    }
}
