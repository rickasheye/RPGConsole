using RPGConsole.Graphical;
using RPGConsole.InventoryItems;
using System;
using System.IO;

namespace RPGConsole.InventoryBlock
{
    public class Dirt : Block
    {
        public Dirt() : base("Dirt", 0, 0, 1, Color.LIGHTBROWN, "Textures/blocks/dirt.png", 1) { giveBlock = false; }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
            if (broken == true)
            {
                if (giveBlock == false)
                {
                    player.inv.addItem(new InventoryItemDirtClump());
                } 
            }
        }
    }
}
