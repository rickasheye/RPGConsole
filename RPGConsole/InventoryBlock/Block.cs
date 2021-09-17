using Raylib_cs;
using RPGConsole.InventoryItems;
using System;

namespace RPGConsole.InventoryBlock
{
    public class Block : InventoryItem
    {
        public enum Color
        {
            BROWN, LIGHTBLUE, LIGHTBROWN, GRAY, GREEN, WHITESKINCOLOR, ORANGE, DARKBROWN, LIGHTGRAY, LIGHTGREEN, PASTEBROWN, SILVER, BLACK
        }

        //block class
        public Vector2 position;
        public int toxicity;
        public int stuckiness;
        public bool broken;
        public int strength = 3;
        public int originalStrength = 3;
        public bool giveBlock = false;
        public Color color;

        public float perlinvalue;
        public Model storedModel;

        public Block(string name, int toxic, int stuck, int strength, Color color, int itemCount) : base(name, itemTYPE.GENERAL, itemCount, entityType.BLOCK)
        {
            this.color = color;
            this.toxicity = toxic;
            this.stuckiness = stuck;
            this.strength = strength;
            originalStrength = strength;
        }

        public Block(string name, int toxic, int stuck, int strength, Color color, int itemCount, Model model):base(name, itemTYPE.GENERAL, itemCount, entityType.BLOCK)
        {
            this.color = color;
            this.toxicity = toxic;
            this.stuckiness = stuck;
            this.strength = strength;
            originalStrength = strength;
            storedModel = model;
        }

        public Block(string name, int toxic, int stuck, int strength, Color color, int itemCount, string texturePath):base(name, itemTYPE.GENERAL, itemCount, entityType.BLOCK, texturePath)
        {
            this.color = color;
            this.toxicity = toxic;
            this.stuckiness = stuck;
            this.strength = strength;
            originalStrength = strength;
        }

        public Block(string name, int toxic, int stuck, int strength, Color color, int itemCount, string texturePath, Model model):base(name, itemTYPE.GENERAL, itemCount, entityType.BLOCK, texturePath)
        {
            this.color = color;
            this.toxicity = toxic;
            this.stuckiness = stuck;
            this.strength = strength;
            originalStrength = strength;
            storedModel = model;
        }

        public Block(string name, int toxic, int stuck, int strength, Color color, string texture, int itemCount):base(name, itemTYPE.GENERAL, itemCount, entityType.BLOCK, texture)
        {
            this.color = color;
            this.toxicity = toxic;
            this.stuckiness = stuck;
            this.strength = strength;
            originalStrength = strength;
        }

        public Block(string name, int toxic, int stuck, int strength, Color color, string texture, int itemCount, Model model):base(name, itemTYPE.GENERAL, itemCount, entityType.BLOCK, texture)
        {
            this.color = color;
            this.toxicity = toxic;
            this.stuckiness = stuck;
            this.strength = strength;
            originalStrength = strength;
            storedModel = model;
        }

        public Block()
        {
            //unfortunately block is emptyt
            //if (Program.debugMode == true) { Program.unit.AddConsoleItem("BLOCK IS EMPTY!!! but is called " + name); }

            if (texture == "Textures/blocks/air.png")
            {
                Program.gen.blockMap.Remove(this);
            }

            if(perlinvalue == 0)
            {
                for(int i = 0; i < Program.gen.blockMap.Count -1; i++)
                {
                    if(Program.gen.blockMap[i].name == name)
                    {
                        Program.gen.blockMap.RemoveAt(i);
                    }
                }
            }
        }

        public virtual void PlayerOnTop()
        {

        }

        public virtual void PlayerOnTop(Player player)
        {
            if(stuckiness > 0)
            {

            }
        }


        public virtual void PlayerOffBlock()
        {

        }

        public virtual void PlayerOffBlock(Player player)
        {

        }

        public virtual void UpdateBlock()
        {

        }

        public virtual void UpdateBlockThroughMiningandPlacing()
        {

        }

        public virtual void MineBlock(Player player)
        {
            if (broken == false)
            {
                bool usingTool = false;
                if (player.equipItem != null)
                {
                    if (player.equipItem.type == itemTYPE.TOOL)
                    {
                        //actually use it
                        usingTool = true;
                    }
                }

                if (strength > 3)
                {
                    if (!usingTool)
                    {
                        Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "you need to use a tool for this block!"));
                        return;
                    }
                }

                if (strength <= 3)
                {
                    strength = 0;
                    broken = true;
                }
                else
                {
                    //get the pick and see if it can break this block!
                    if (player.equipItem is InventoryItemPickaxe)
                    {
                        //it is a pickaxe so get the damage level
                        InventoryItemPickaxe pick = player.equipItem as InventoryItemPickaxe;
                        strength = strength - pick.damageLevel;
                    }
                    else
                    {
                        Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "You do not have a pickaxe equipped to be used on this block!"));
                    }
                }

                if (broken == false) { return; }
                if (broken == true && strength <= 0) { if (giveBlock == true) { player.inv.addItem(this); } }
            }
            else
            {
                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "This block seems to be already broken"));
            }
        }
    }
}
