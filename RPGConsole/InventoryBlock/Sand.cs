using RPGConsole.Graphical;
using RPGConsole.InventoryItems;
using System;
using System.IO;

namespace RPGConsole.InventoryBlock
{
    public class Sand : Block
    {
        public Sand() : base("Sand", 0, 0, 1, Color.SILVER, "Textures/blocks/sand.png", 1) { giveBlock = true; }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
        }
    }
}
