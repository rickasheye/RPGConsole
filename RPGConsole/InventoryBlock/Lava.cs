using Raylib_cs;
using RPGConsole.Graphical;
using RPGConsole.InventoryItems;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace RPGConsole.InventoryBlock
{
    public class Lava : Block
    {
        public Lava() : base("Lava", 2, 0, -1, Color.ORANGE, "Textures/blocks/lava.png", 1) { }

        public override void PlayerOnTop(Player player)
        {
            base.PlayerOnTop(player);
            //Take stuff from the player!
            int tempTimer = 0;
            while (player.position.x == position.x && player.position.y == position.y)
            {
                //take health from the player!
                player.health -= toxicity;
                Program.unit.AddConsoleItem(new ConsoleItem(3, "you are sitting on lava it has depleted your health by " + toxicity));

                if (player.health <= 0)
                {
                    break;
                }
                if (Program.cmdMode) { Thread.Sleep(800); }
                Random randChance = new Random();
                int randomChance = randChance.Next(2);
                if (randomChance == 1)
                {
                    player.MovePlayer((int)player.position.x + 1, (int)player.position.y);
                    Program.unit.AddConsoleItem(new ConsoleItem(3, "luckily you have survived and made it out of the lava!"));
                }
            }
        }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
            if (!giveBlock)
            {
                player.inv.addItem(new InventoryItemLavaClump());
            }
        }

        public override void UpdateBlockThroughMiningandPlacing()
        {
            base.UpdateBlockThroughMiningandPlacing();
            //producing obsidian or modifying blocks on there
            Generator generator = Program.gen;
            bool connectedToWater = false;
            Vector2 newPosition = new Vector2(0, 0);
            if(generator.returnBlock(position.x + 1, position.y) == generator.getBlock("Water"))
            {
                newPosition = new Vector2((int)position.x + 1, position.y);
                connectedToWater = true;
            }else if(generator.returnBlock(position.x, position.y + 1) == generator.getBlock("Water"))
            {
                newPosition = new Vector2(position.x, position.y + 1);
                connectedToWater = true;
            }else if(generator.returnBlock(position.x - 1, position.y) == generator.getBlock("Water"))
            {
                newPosition = new Vector2(position.x - 1, position.y);
                connectedToWater = true;
            }else if(generator.returnBlock(position.x, position.y-1) == generator.getBlock("Water"))
            {
                newPosition = new Vector2(position.x, position.y - 1);
                connectedToWater = true;
            }

            if(connectedToWater == true)
            {
                generator.setBlock((int)newPosition.x, (int)newPosition.y, new Obsidian());
                generator.setBlock((int)position.x, (int)position.y, new Rock());
            }
        }
    }
}
