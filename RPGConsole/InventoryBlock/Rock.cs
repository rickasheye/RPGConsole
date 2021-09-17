using Raylib_cs;
using RPGConsole.Graphical;
using RPGConsole.InventoryItems;
using System;
using System.IO;

namespace RPGConsole.InventoryBlock
{
    public class Rock : Block
    {
        public Rock() : base("Rock", 0, 0, 10, Color.LIGHTGRAY, "Textures/blocks/rock.png", 1, Raylib.LoadModel("Models/otherrocks.obj")) { }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
            if (broken == true)
            {
                if (!giveBlock)
                {
                    player.inv.addItem(new InventoryItemRockFragments());
                } 
            }
        }
    }
}
