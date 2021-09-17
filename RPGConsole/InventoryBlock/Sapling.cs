using RPGConsole.Graphical;
using RPGConsole.InventoryItems;
using System;
using System.IO;

namespace RPGConsole.InventoryBlock
{
    public class Sapling : Block
    {
        int section = 0; 
        public Sapling() : base("Sapling", 0, 0, 1, Color.LIGHTGREEN, "Textures/blocks/sapling.png", 1) { giveBlock = false; }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
            //give some random amount of wood and leaves
            if (giveBlock == false)
            {
                if (broken)
                {
                    Random rnad = new Random();
                    int randomAmount = rnad.Next(10);
                    for (int i = 0; i < randomAmount; i++)
                    {
                        player.inv.addItem(new Wood());
                    }
                    int randomAmountLeaves = rnad.Next(10);
                    for (int i = 0; i < randomAmountLeaves; i++)
                    {
                        player.inv.addItem(new InventoryItemLeaf());
                    }  
                }
            }
        }

        int timer = 0;
        public override void UpdateBlock()
        {
            base.UpdateBlock();
            timer++;
            if(timer > 400)
            {
                section++;
                timer = 0;
            }
        }
    }
}
