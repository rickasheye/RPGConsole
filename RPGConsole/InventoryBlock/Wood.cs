using RPGConsole.Graphical;
using System;
using System.IO;

namespace RPGConsole.InventoryBlock
{
    public class Wood : Block
    {
        public Wood() : base("Wood", 0, 0, 1, Color.PASTEBROWN, "Textures/blocks/wood.png", 1) { giveBlock = true; }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
        }
    }
}
