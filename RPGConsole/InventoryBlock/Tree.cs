using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.InventoryBlock
{
    public class Tree : Block
    {
        public Texture2D textureTreeBush;

        public Tree() : base("Tree", 0, 0, 1, Color.LIGHTGREEN, "Textures/blocks/sapling.png", 1) { 
            giveBlock = false;
            if(textureTreeBush.width != 0)
            {
                //?? huh its supposed to be 0
                Program.unit.AddConsoleItem("Huh? this tree bush texture has data in it?");
            }
            else
            {
                Program.unit.AddConsoleItem("Loading treebush texture");
                
            }
        }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
            //give some random amount of wood and leaves
            if (giveBlock == false)
            {
                if (broken)
                {
                    //snap
                }
            }
        }

        public override void UpdateBlock()
        {
            base.UpdateBlock();
            //draw the tree texture on top
            if(Vector2.Distance(Program.player.position, position) < 8)
            {
                //make the tree opaque
            }
        }
    }
}