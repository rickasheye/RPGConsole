using DSharpPlus.Entities;
using Raylib_cs;
using RPGConsole.Crafting;
using RPGConsole.Graphical.MenuItems;
using RPGConsole.Graphical.MenuItems.KeyboardOnlyItems;
using RPGConsole.InventoryBlock;
using RPGConsole.InventoryItems;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Threading;

namespace RPGConsole
{
    public class Player
    {
        public string name;
        public Vector2 position;
        public Inventory inv;
        public int health = 30;
        public InventoryItem equipItem = new Air();
        public int level = 1;

        public bool craftingTable;
        public bool furnace;
        //public Block ontopOfBlock = null;

        public ulong discordid;
        public Player()
        {
            if (Program.gen != null) { position = new Vector2(Program.gen.sizeMapX / 2, Program.gen.sizeMapY / 2); } else { position = new Vector2(0, 0); }
            if (inv == null)
            {
                inv = new Inventory();
            }
            if (name == null)
            {
                name = "untitled player";
            }
            MovePlayer((int)position.x, (int)position.y);
        }

        public void MovePlayer(int x, int y, DiscordMessage message)
        {
            bool enemiesNearby = false;
            Vector2 originalPos = new Vector2(position.x, position.y);
            Vector2 newPos = new Vector2(x, y);
            if (originalPos.x != newPos.x || originalPos.y != newPos.y)
            {
                if (x >= 0 && y >= 0 && x <= Program.gen.sizeMapX && y <= Program.gen.sizeMapY)
                {
                    if (Program.gen.returnBlock(newPos.x, newPos.y) != null)
                    {
                        //Console.WriteLine("You have landed on the block: " + Program.gen.returnBlock(newPos.x, newPos.y).name);
                        for (int xPos = -2; xPos < 2; xPos++)
                        {
                            for (int yPos = -2; yPos < 2; yPos++)
                            {
                                Block returnedBlock = Program.gen.returnBlock(position.x + xPos, position.y + yPos);
                                if (returnedBlock as Grass != null)
                                {
                                    if (!enemiesNearby)
                                    {
                                        Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "there maybe enemies nearby..."), message);
                                        enemiesNearby = true;
                                    }
                                }
                            }
                        }
                    }

                    Block getBlock = Program.gen.returnBlock(newPos.x, newPos.y);
                    if (getBlock != null)
                    {
                        position = newPos;
                        Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "moved player from " + originalPos.ToString() + " to " + position.ToString()), message);
                        getBlock.PlayerOnTop(this);
                        getBlock.PlayerOnTop();

                        Block oldBlock = Program.gen.returnBlock(originalPos.x, originalPos.y);
                        oldBlock.PlayerOffBlock();
                        oldBlock.PlayerOffBlock(this);
                    }
                    else
                    {
                        Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "unable to move since that block doesnt exist!"), message);
                    } 
                }
            }
        }

        public void MovePlayer(int x, int y)
        {
            MovePlayer(x, y, null);
        }

        public InventoryItem inventoryGet(string name)
        {
            return inv.items.Find(t => t.name == name);
        }

        public bool equipInventoryItem(string name)
        {
            InventoryItem item = inventoryGet(name);
            return eqipInventoryItem(item);
        }

        public bool eqipInventoryItem(InventoryItem item)
        {
            equipItem = item;
            Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "equipped the item: " + item.name));
            return true;
        }

        int levelupTime = 0;

        public void LevelUp()
        {
            if (levelupTime > level * 2)
            {
                //level up
                level++;
                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "Congrats player! you have levelled up!"));
            }
            else
            {
                levelupTime++;
            }
        }

        public List<CraftingRecipe> recipes = new List<CraftingRecipe>();
        public List<FurnaceRecipe> furnaceRecipes = new List<FurnaceRecipe>();

        void SetupCrafting()
        {
            recipes.Add(new CraftingRecipe(new InventoryItemSticks(1), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemStoneSword(), true));
            recipes.Add(new CraftingRecipe(new Wood(), new Wood(), new Wood(), new Wood(), new CraftingTable(), false));
            recipes.Add(new CraftingRecipe(new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new Furnace(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemDiamondClump(), new InventoryItemDiamondClump(), new InventoryItemSticks(1), new InventoryItemDiamondSword(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemIronClump(), new InventoryItemIronClump(), new InventoryItemSticks(1), new InventoryItemMetalSword(), true));
            recipes.Add(new CraftingRecipe(new Wood(), new Wood(), new InventoryItemSticks(4), false));
            recipes.Add(new CraftingRecipe(new InventoryItemDiamondClump(), new InventoryItemDiamondClump(), new InventoryItemDiamondClump(), new InventoryItemSticks(1), new InventoryItemSticks(1), new InventoryItemDiamondPickaxe(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemIronClump(), new InventoryItemIronClump(), new InventoryItemIronClump(), new InventoryItemSticks(1), new InventoryItemSticks(1), new InventoryItemMetalPickaxe(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemSticks(1), new InventoryItemSticks(1), new InventoryItemStonePickaxe(), true));
            recipes.Add(new CraftingRecipe(new Wood(), new Wood(), new Wood(), new InventoryItemSticks(1), new InventoryItemSticks(1), new InventoryItemWoodPickaxe(), true));
            recipes.Add(new CraftingRecipe(new Wood(), new Wood(), new Wood(), new Wood(), new Wood(), new Wood(), new Wood(), new Wood(), new Chest(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new BlankMachinePart(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemRockFragments(), new InventoryItemSticks(1), new ElectrodeNode(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemElectrode(3), new BlankMachinePart(), new MachinePart_Power(), true));
        }

        void SetupFurnace()
        {
            furnaceRecipes.Add(new FurnaceRecipe(new DiamondOre(), new InventoryItemDiamondClump()));
            furnaceRecipes.Add(new FurnaceRecipe(new IronOre(), new InventoryItemIronClump()));
        }

        public List<CraftingRecipe> recipesCalc()
        {
            List<CraftingRecipe> allowedReceipes = new List<CraftingRecipe>();
            for (int i = 0; i < recipes.Count; i++)
            {
                List<InventoryItem> item = CraftingRecipe.convertToList(recipes[i]);
                bool acceptable = true;
                if (recipes[i].needTable == true && craftingTable == false)
                {
                    acceptable = false;
                }

                if (acceptable == false)
                {
                    for (int z = 0; z < item.Count; z++)
                    {
                        if (inv.itemExists(item[z]) == false)
                        {
                            acceptable = false;
                        }
                        else
                        {
                            if (inv.itemFind(item[z]).itemCount >= item[z].itemCount)
                            {
                                acceptable = true;
                            }
                        }
                    }
                }

                if (acceptable == true)
                {
                    allowedReceipes.Add(recipes[i]);
                }
            }
            return allowedReceipes;
        }

        public bool crafting = false;
        public void InitiateCrafting()
        {
            //Start the crafting!!!
            crafting = true;
            if (Program.discordBot)
            {
                if (Program.debugMode) { Program.unit.AddConsoleItem("Discord bot is set to true unfortunately so remove itself."); }
            }
            while (crafting && !Program.discordBot)
            {
                if (Program.cmdMode)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to crafting please pick a argument to do an action!");
                }

                //calculate things!
                if (recipes.Count <= 0)
                {
                    SetupCrafting();
                }
                bool unfortunate = true;
                List<CraftingRecipe> allowedReceipes = recipesCalc();
                if(allowedReceipes != null)
                {
                    unfortunate = false;
                }

                if (Program.cmdMode)
                {
                    if (unfortunate == true)
                    {
                        Program.unit.AddConsoleItem("Unfortunately you do not have any valid materials to craft with!");
                    }
                    else
                    {
                        for (int i = 0; i < allowedReceipes.Count; i++)
                        {
                            Program.unit.AddConsoleItem("(" + i + ") - " + allowedReceipes[i].output.name);
                        }
                    }

                    if (!Program.discordBot)
                    {
                        Program.unit.AddConsoleItem("give command (exit) to quit out of crafting prompt!");
                        string craftingVariable = Console.ReadLine().ToLower();
                        switch (craftingVariable)
                        {
                            case "quit":
                            case "exit":
                            case "goodbye":
                            case "bye":
                            case "stop":
                                crafting = false;
                                Program.unit.AddConsoleItem("exited crafting...");
                                break;
                        }
                        int finalConvertedInteger = 0;
                        bool check = int.TryParse(craftingVariable, out finalConvertedInteger);
                        if (check == true)
                        {
                            List<InventoryItem> item = CraftingRecipe.convertToList(allowedReceipes[finalConvertedInteger]);
                            foreach (InventoryItem itm in item)
                            {
                                inv.removeItem(itm);
                            }
                            inv.addItem(allowedReceipes[finalConvertedInteger].output);
                        }
                        else
                        {
                            if (crafting == true)
                            {
                                Program.unit.AddConsoleItem("Unfortunately that number is incorrect!");
                            }
                        } 
                    }
                }
                else
                {
                    //Display a graphical menu
                    Program.loader.currentScene.guiOptions.Clear();
                    if (allowedReceipes.Count > 0)
                    {
                        Program.loader.currentScene.guiOptions.Add(new Text("Welcome to crafting use the following menu to craft!", new Vector2(10, 10), 5, Color.BLACK));
                        for (int i = 0; i < allowedReceipes.Count; i++)
                        {
                            CraftingGUIOption guiOption = new CraftingGUIOption(allowedReceipes[i].output.name, new Vector2(10, (10 * i) + 25), new Vector2(50, 50));
                            guiOption.giveItem = allowedReceipes[i].output;
                            guiOption.removingItems = CraftingRecipe.convertToList(allowedReceipes[i]);
                            Program.loader.currentScene.guiOptions.Add(guiOption);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No recipes to show!");
                        Text text = new Text("No recipes to show use 'C' again to exit!", new Vector2(10, 10), 5, Color.BLACK);
                        Program.loader.currentScene.guiOptions.Add(text);
                    }

                    EmptyContainer container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
                    container.children.AddRange(Program.loader.currentScene.guiOptions);
                    Program.loader.currentScene.guiOptions.Add(container);
                    crafting = false;
                }
            }
        }

        public void InitiateFurnacing()
        {
            if (furnaceRecipes.Count <= 0)
            {
                SetupFurnace();
            }

            bool furnaceBaking = true;
            if (furnace == false)
            {
                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "unable to initate furnace since you are not sitting ontop of a furnace!"));
                furnaceBaking = false;
            }
            while (furnaceBaking)
            {
                if (Program.cmdMode || Program.discordBot)
                {
                    if (!Program.discordBot) { Console.Clear(); }
                    Program.unit.AddConsoleItem("Welcome to furnacing select an argument to furnace an item..."); 
                }
                List<FurnaceRecipe> recipes = new List<FurnaceRecipe>();
                for (int i = 0; i < furnaceRecipes.Count; i++)
                {
                    FurnaceRecipe recipe = furnaceRecipes[i];
                    bool valid = false;
                    if (inv.itemExists(recipe.input))
                    {
                        //allow to provide the output!
                        recipes.Add(recipe);
                    }
                }

                if (Program.cmdMode || Program.discordBot)
                {
                    if (recipes.Count > 0)
                    {
                        for (int i = 0; i < recipes.Count; i++)
                        {
                            Program.unit.AddConsoleItem("(" + i + ") - " + recipes[i].output);
                        }
                    }
                    else
                    {
                        Program.unit.AddConsoleItem("You are unable to use the furnace due to no possible materials to use!");
                    }
                    if (!Program.discordBot) { FurnaceChoose(Console.ReadLine(), ref furnaceBaking, recipes); }
                }
                else
                {
                    Program.loader.currentScene.guiOptions.Clear();
                    if (recipes.Count > 0)
                    {
                        Program.loader.currentScene.guiOptions.Add(new Text("Welcome to the furnace use the following menu to furnace!", new Vector2(10, 10), 5, Color.BLACK));
                        for (int i = 0; i < recipes.Count; i++)
                        {
                            FurnaceRecipe recipie = recipes[i];
                            FurnaceGUIOption furnaceOption = new FurnaceGUIOption(recipie.input.name + " > " + recipie.output.name, new Vector2(10, (10 * i) + 25), new Vector2(50, 50));
                            furnaceOption.input = recipie.input;
                            furnaceOption.output = recipie.output;
                            Program.loader.currentScene.guiOptions.Add(furnaceOption);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No furnace recipes to show!");
                        Text text = new Text("No recipes to show use 'F' again to exit!", new Vector2(10, 10), 5, Color.BLACK);
                        Program.loader.currentScene.guiOptions.Add(text);
                    }
                    EmptyContainer container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
                    container.children.AddRange(Program.loader.currentScene.guiOptions);
                    Program.loader.currentScene.guiOptions.Add(container);
                }
                furnaceBaking = false;
            }
        }

        public void FurnaceChoose(string furnaceVariable, ref bool furnaceBaking, List<FurnaceRecipe> recipes)
        {
            Program.unit.AddConsoleItem("give command (exit) to quit out of furnace prompt!");
            switch (furnaceVariable)
            {
                case "quit":
                case "exit":
                case "goodbye":
                case "bye":
                case "stop":
                    furnaceBaking = false;
                    Program.unit.AddConsoleItem("exited furnacing...");
                    break;
            }
            int finalConvertedInteger = 0;
            bool check = int.TryParse(furnaceVariable, out finalConvertedInteger);
            if (check == true)
            {
                Program.unit.AddConsoleItem("Cooking...");
                if (!Program.discordBot) { Thread.Sleep(800); }
                inv.removeItem(recipes[finalConvertedInteger].input);
                inv.addItem(recipes[finalConvertedInteger].output);
                Program.unit.AddConsoleItem("Cooked!");
            }
            else
            {
                if (furnaceBaking == true)
                {
                    Program.unit.AddConsoleItem("Unfortunately that number is incorrect!");
                }
            }
        }

        public Chest chest;

        public void InitiateInventoryGUI()
        {
            if (!Program.cmdMode)
            {
                Program.loader.currentScene.guiOptions.Clear();
                //display the inventory
                if (inv.items.Count > 0)
                {
                    Program.loader.currentScene.guiOptions.Add(new Text("INVENTORY", new Vector2(10, 10), 5, Color.BLACK));
                    //display the actual inventory with a gui
                    for (int i = 0; i < inv.items.Count; i++)
                    {
                        if (inv.items[i].itemCount > 0)
                        {
                            string modernName = inv.items[i].name;
                            if (equipItem == inv.items[i])
                            {
                                modernName = modernName + " EQUIPPED!";
                            }
                            InventoryGUIOption invOption = new InventoryGUIOption(modernName + " #" + inv.items[i].itemCount, new Vector2(10, (10 * i) + 25), new Vector2(50, 50), chest);
                            invOption.eqipItem = inv.items[i];
                            Program.loader.currentScene.guiOptions.Add(invOption); 
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No inventory items to show!");
                    Text text = new Text("No items to show use 'I' again to exit!", new Vector2(10, 10), 5, Color.BLACK);
                    Program.loader.currentScene.guiOptions.Add(text);
                }
                EmptyContainer container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
                container.children.AddRange(Program.loader.currentScene.guiOptions);
                Program.loader.currentScene.guiOptions.Add(container);
            }
            else
            {
                Program.unit.AddConsoleItem("Inventory Items", 3);
                foreach(InventoryItem item in inv.items)
                {
                    Program.unit.AddConsoleItem(item.name + " " + item.itemCount, 3);
                }
            }
        }

        public void InitiateInventoryChest()
        {
            if (!Program.cmdMode)
            {
                Program.loader.currentScene.guiOptions.Clear();
                //display the inventory
                if (chest.storeInventoryItems.Count > 0)
                {
                    Program.loader.currentScene.guiOptions.Add(new Text("CHEST", new Vector2(10, 10), 5, Color.BLACK));
                    //display the actual inventory with a gui
                    for (int i = 0; i < chest.storeInventoryItems.Count; i++)
                    {
                        InventoryChestGUIOption invOption = new InventoryChestGUIOption(chest.storeInventoryItems[i].name, new Vector2(10, (10 * i) + 25), new Vector2(50, 50));
                        invOption.eqipItem = chest.storeInventoryItems[i];
                        Program.loader.currentScene.guiOptions.Add(invOption);
                    }
                }
                else
                {
                    Console.WriteLine("No chest items to show!");
                    Text text = new Text("No items to show in this chest! use the Inventory to put items in this chest!", new Vector2(10, 10), 5, Color.BLACK);
                    Program.loader.currentScene.guiOptions.Add(text);
                }
                EmptyContainer container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
                container.children.AddRange(Program.loader.currentScene.guiOptions);
                Program.loader.currentScene.guiOptions.Add(container);
            }
            else
            {
                //just spawn the inventory inside of this!
                Program.unit.AddConsoleItem("Chest inventory", 3);
                foreach(InventoryItem item in chest.storeInventoryItems)
                {
                    Program.unit.AddConsoleItem(item.name + " " + item.itemCount, 3);
                }
            }
        }
    }

    public class CraftingGUIOption : KeyboardAdjustedButton
    {
        public List<InventoryItem> removingItems;
        public InventoryItem giveItem;

        public CraftingGUIOption(string title, Vector2 pos, Vector2 size):base(title, size, pos) { }

        public override void Triggerable()
        {
            base.Triggerable();
            foreach(InventoryItem item in removingItems)
            {
                Program.player.inv.removeItem(item);
            }
            Program.player.inv.addItem(giveItem);
            Program.loader.currentScene.guiOptions.Clear();
            Program.player.InitiateCrafting();
        }
    }

    public class FurnaceGUIOption : KeyboardAdjustedButton
    {
        public InventoryItem input;
        public InventoryItem output;
        public FurnaceGUIOption(string title, Vector2 pos, Vector2 size):base(title, size, pos){}

        public override void Triggerable()
        {
            base.Triggerable();
            Program.loader.currentScene.guiOptions.Clear();
            Program.loader.currentScene.guiOptions.Add(new Text("Cooking!", new Vector2(10, 10), 5, Color.BLACK));
            Thread.Sleep(800);
            Program.player.inv.removeItem(input);
            Program.player.inv.addItem(output);
            Program.loader.currentScene.guiOptions.Clear();
            Program.loader.currentScene.guiOptions.Add(new Text("Cooked!", new Vector2(10, 10), 5, Color.BLACK));
            //update the gui
            Program.loader.currentScene.guiOptions.Clear();
            Program.player.InitiateFurnacing();
        }
    }

    public class InventoryGUIOption : KeyboardAdjustedButtonOptional
    {
        public InventoryItem eqipItem;
        Chest chestInstance;

        public InventoryGUIOption(string title, Vector2 pos, Vector2 size, Chest chestInstance):base(title, size, pos) { if (chestInstance == null) { this.chestInstance = chestInstance; justbenormal = true; } }

        public override void Start()
        {
            base.Start();
            if (justbenormal == false)
            {
                //add the children to this object!.
                if (chestInstance != null)
                {
                    InventoryGUIEquip guiEqip = new InventoryGUIEquip("Equip Item", position, size);
                    guiEqip.eqipItem = eqipItem;
                    children.Add(guiEqip);
                    InventoryGUIChestOption chestOption = new InventoryGUIChestOption("Dump this item in a chest!", position, size);
                    chestOption.itemEquippable = eqipItem;
                    chestOption.chestInstance = chestInstance;
                    children.Add(chestOption);
                }
                else
                {
                    //nothing i guess
                    justbenormal = true;
                } 
            }
        }

        public override void Triggerable()
        {
            if (justbenormal == true)
            {
                if (eqipItem == null)
                {
                    Console.WriteLine("Unfortunately this item was unavaliable!");
                }
                base.Triggerable();
                Program.player.equipInventoryItem(eqipItem.name);
                Program.loader.currentScene.guiOptions.Clear();
                Program.player.InitiateInventoryGUI(); 
            }
        }
    }

    public class InventoryGUIEquip : KeyboardAdjustedButton
    {
        public InventoryItem eqipItem;

        public InventoryGUIEquip(string title, Vector2 pos, Vector2 size):base(title, size, pos) { }

        public override void Triggerable()
        {
            if (eqipItem == null)
            {
                Console.WriteLine("Unfortunately this item was unavaliable!");
            }
            base.Triggerable();
            Program.player.equipInventoryItem(eqipItem.name);
            Program.loader.currentScene.guiOptions.Clear();
            Program.player.InitiateInventoryGUI();
        }
    }

    public class InventoryGUIChestOption : KeyboardAdjustedButton
    {
        public Chest chestInstance;
        public InventoryItem itemEquippable;

        public InventoryGUIChestOption(string text, Vector2 pos, Vector2 size) : base(text, size, pos) { }

        public override void Triggerable()
        {
            base.Triggerable();
            chestInstance.RemoveChestItem(itemEquippable);
            Program.player.inv.addItem(itemEquippable);
            Program.loader.currentScene.guiOptions.Clear();
            Program.player.InitiateInventoryGUI();
        }
    }

    public class InventoryChestGUIOption : KeyboardAdjustedButton
    {
        public InventoryItem eqipItem;
        public Chest chestInstance;

        public InventoryChestGUIOption(string title, Vector2 pos, Vector2 size):base(title, size, pos) { }

        public override void Triggerable()
        {
            if (eqipItem == null)
            {
                Console.WriteLine("Unfortunately this item was unavaliable!");
            }
            base.Triggerable();
            Program.player.inv.addItem(eqipItem);
            chestInstance.RemoveChestItem(eqipItem);
            Program.loader.currentScene.guiOptions.Clear();
            Program.player.InitiateInventoryChest();
        }
    }
}
