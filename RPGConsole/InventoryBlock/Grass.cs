using Raylib_cs;
using RPGConsole.GameEnemies;
using RPGConsole.Graphical;
using System;
using System.Drawing;
using System.IO;

namespace RPGConsole.InventoryBlock
{
    public class Grass : Block
    {
        int randomINT = 0;

        public Grass() : base("Grass", 0, 0, 2, Color.GREEN, "Textures/blocks/grass.png", 1) {

            Random random = new Random();
            int randNext = random.Next(10);
            if (randNext == 1)
            {
                //Then spawn an enemy
                Random rand = new Random();
                int rnadomEnemy = rand.Next(2);
                randomINT = rnadomEnemy;
            }
        }

        public override void UpdateBlock()
        {
            base.UpdateBlock();
            switch (randomINT)
            {
                case 0:
                    Random randRectal = new Random();
                    int rnadRects = randRectal.Next(5);
                    for (int i = 0; i < rnadRects; i++)
                    {
                        Random rndX = new Random();
                        int rnadX = rndX.Next((int)position.x, (int)position.x + 64);
                        Random rndY = new Random();
                        int rnadY = rndY.Next((int)position.y, (int)position.y + 64);
                        Random rndXSize = new Random();
                        Random rndYSize = new Random();
                        int rnadXSize = rndXSize.Next(2, 5);
                        int rnadYSize = rndYSize.Next(2, 5);
                        Raylib.DrawRectangle(rnadX, rnadY, rnadXSize, rnadYSize, Raylib_cs.Color.BLACK); 
                    }
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    Random randCircle = new Random();
                    int rnadCircles = randCircle.Next(5);
                    for (int i = 0; i < rnadCircles; i++)
                    {
                        Random rndX = new Random();
                        int rnadX = rndX.Next((int)position.x, (int)position.x + 64);
                        Random rndY = new Random();
                        int rnadY = rndY.Next((int)position.y, (int)position.y + 64);
                        Random rndXSize = new Random();
                        Random rndYSize = new Random();
                        int rnadXSize = rndXSize.Next(2, 5);
                        int rnadYSize = rndYSize.Next(2, 5);
                        Raylib.DrawCircle(rnadX, rnadY, rnadXSize + rnadYSize, Raylib_cs.Color.BLACK);
                    }
                    break;
            }
        }

        public override void PlayerOnTop(Player player)
        {
            base.PlayerOnTop(player);
            //give the player a random chance
            if (player.equipItem.type == InventoryItems.itemTYPE.WEAPON)
            {
                switch (randomINT)
                {
                    case 0:
                        //No enemy
                        Console.WriteLine("there seems to be footprints of a nearby enemy here!");
                        break;
                    case 1:
                        //start the bug up!
                        Bug bug = new Bug(position, player);
                        break;
                    case 2:
                        Bear bear = new Bear(position, player);
                        break;
                    default:
                        Console.WriteLine("there seems to be reminants of a beast laying here!");
                        break;
                }
            }
        }
    }
}
