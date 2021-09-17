using Raylib_cs;
using RPGConsole.Graphical;
using RPGConsole.InventoryItems;
using System;
using System.IO;
using System.Threading;

namespace RPGConsole.InventoryBlock
{
    public class Water : Block
    {
        public Water() : base("Water", 0, 1, -1, Color.LIGHTBLUE, "Textures/blocks/water.png", 1) { }

        public override void PlayerOnTop(Player player)
        {
            base.PlayerOnTop(player);
            //Take stuff from the player!
        }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
            if (!giveBlock)
            {
                player.inv.addItem(new InventoryItemWaterClump());
            }
        }
    }
}
