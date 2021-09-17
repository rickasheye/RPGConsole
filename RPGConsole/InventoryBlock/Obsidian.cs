using RPGConsole.Graphical;
using RPGConsole.InventoryItems;
using System;
using System.IO;

namespace RPGConsole.InventoryBlock
{
    public class Obsidian : Block
    {
        public Obsidian() : base("Obsidian", 0, 0, 1, Color.BLACK, "Textures/blocks/obsidian.png", 1) { giveBlock = true; }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
        }
    }
}
