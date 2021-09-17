using RPGConsole.Graphical;
using RPGConsole.InventoryItems;
using System;
using System.IO;

namespace RPGConsole.InventoryBlock
{
    public class DiamondOre : Block
    {
        public DiamondOre() : base("Diamond Ore", 0, 0, 30, Color.LIGHTBLUE, "Textures/blocks/diamondore.png", 1) { giveBlock = true; }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
            if (broken)
            {
                if (!giveBlock)
                {
                    player.inv.addItem(new InventoryItemDiamondClump());
                } 
            }
        }
    }
}
