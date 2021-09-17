using Raylib_cs;
using RPGConsole.EngineStuff;
using RPGConsole.Graphical.MenuItems;
using RPGConsole.Graphical.MenuItems.KeyboardOnlyItems;
using RPGConsole.InventoryItems;
using System;
using System.Threading;

namespace RPGConsole.GameEnemies
{
    public class Enemy : GameObject
    {
        public int health = 20;
        public bool playerStuck;

        public Enemy(string name, Vector2 position, Player player) : base(name, position)
        {
            if (Program.cmdMode) { StartSequence(player); } else
            {
                StartGUISequence();
            }
        }

        public void StartSequence(Player player)
        {
            //Sequence start
            bool playerReady = true;
            Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, name + " has decided to attack!"));
            while (playerReady == true)
            {
                //stop the player
                string lines = Console.ReadLine();
                string[] args = lines.Split(" ");
                switch (args[0])
                {
                    case "equipment":
                    case "equip":
                    case "e":
                        //equip an item from a inventory!
                        if (args.Length > 1)
                        {
                            bool itemEquipped = player.equipInventoryItem(args[1]);
                            if (itemEquipped == false)
                            {
                                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "Item was not equipped continuing"));
                            }
                        }
                        else
                        {
                            Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "args are incorrect please try again"));
                        }
                        break;
                    case "fight":
                    case "engage":
                        if (player.equipItem.type == itemTYPE.WEAPON)
                        {
                            //add this onto the pressure.
                            InventoryItemWeapon weapon = (InventoryItemWeapon)player.equipItem;
                            if (weapon == null)
                            {
                                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "program error: unable to convert to weapon!!!"));
                            }
                            else
                            {
                                Random randLevel = new Random();
                                int randdmg = randLevel.Next(weapon.damageLevel);
                                health -= randdmg;
                                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "Your weapon called " + weapon.name + " has delt " + randdmg + " to the enemy!"));
                                weapon.UpgradeWeapon(player);
                            }
                        }
                        break;
                    case "run":
                        Random randNew = new Random();
                        int randomValue = randNew.Next(10);
                        if (randomValue == 5)
                        {
                            //run
                            Program.unit.AddConsoleItem("you have sucessfully ran from the enemy", 3);
                            Thread.Sleep(300);
                            if (playerReady == true)
                            {
                                playerReady = false;
                                Program.loader.currentScene.guiOptions.Clear();
                            }
                            else
                            {
                                Program.unit.AddConsoleItem("thats unfortunate? the player is stuck but the boolean has been set to false!", 3);
                            }
                        }
                        else
                        {
                            //unfortunately you have not escaped!
                            Program.unit.AddConsoleItem("unfortunately you have not escaped!", 3);
                        }
                        break;
                    case "help":
                        Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "HELP COMMANDS!"));
                        Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "( equipment/equip/e <itemname> ) to equip an item during battle! watch out as soon as you execute a command fully the enemy has a stab at you!"));
                        Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "( fight/engage ) to fight or engage on the enemy!"));
                        break;
                    default:
                        Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "that is not a valid command! please execute 'help' for more information!"));
                        break;
                }
                //take a break and let the enemy fight the player
                Random rand = new Random();
                int randValue = rand.Next(player.level * 2);
                player.health -= randValue;
                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "enemy has delt " + randValue + " damage"));
                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "your health is now at " + player.health));

                //break the process if player health is too low!
                if (player.health <= 0)
                {
                    playerReady = false;
                }
            }
        }

        public void StartGUISequence()
        {
            Program.loader.currentScene.guiOptions.Clear();
            Program.loader.currentScene.guiOptions.Add(new Text("An enemy has decided to attack you!", new Vector2(10, 10), 20, Color.BLACK));
            Program.loader.currentScene.guiOptions.Add(new EnemyGUIButton(this, "Attack the " + name, new Vector2(20, 20), new Vector2(10, 40)));
            Program.loader.currentScene.guiOptions.Add(new EnemyRunGUIButton(this, "Run from the " + name, new Vector2(20, 20), new Vector2(10, 70)));
            EmptyContainer container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
            container.children.AddRange(Program.loader.currentScene.guiOptions);
            Program.loader.currentScene.guiOptions.Add(container);
        }
    }

    public class EnemyGUIButton : KeyboardAdjustedButton
    {
        public Enemy enemyConcern;

        public EnemyGUIButton(Enemy enemyConcern, string name, Vector2 size, Vector2 position):base(name, size, position)
        {
            this.enemyConcern = enemyConcern;
        }

        public override void Triggerable()
        {
            base.Triggerable();
            if (Program.player.equipItem.type == itemTYPE.WEAPON)
            {
                //add this onto the pressure.
                InventoryItemWeapon weapon = (InventoryItemWeapon)Program.player.equipItem;
                if (weapon == null)
                {
                    Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "program error: unable to convert to weapon!!!"));
                }
                else
                {
                    Random randLevel = new Random();
                    int randdmg = randLevel.Next(weapon.damageLevel);
                    enemyConcern.health -= randdmg;
                    Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "Your weapon called " + weapon.name + " has delt " + randdmg + " to the enemy!"));
                    weapon.UpgradeWeapon(Program.player);
                }
            }
        }
    }

    public class EnemyRunGUIButton : KeyboardAdjustedButton
    {
        public Enemy enemyConcern;

        public EnemyRunGUIButton(Enemy enemyConcern, string name, Vector2 size, Vector2 position):base(name, size, position) { this.enemyConcern = enemyConcern; }

        public override void Triggerable()
        {
            base.Triggerable();
            Random rand = new Random();
            int randomValue = rand.Next(10);
            if(randomValue == 5)
            {
                //run
                Program.unit.AddConsoleItem("you have sucessfully ran from the enemy", 3);
                Program.loader.currentScene.guiOptions.Add(new Text("You have sucessfully escaped from the enemy!", new Vector2(10, 10), 20, Color.BLACK));
                Thread.Sleep(300);
                if (enemyConcern.playerStuck == true)
                {
                    enemyConcern.playerStuck = false;
                    Program.loader.currentScene.guiOptions.Clear();
                }
                else
                {
                    Program.unit.AddConsoleItem("thats unfortunate? the player is stuck but the boolean has been set to false!", 3);
                }
            }
            else
            {
                //unfortunately you have not escaped!
                Program.unit.AddConsoleItem("The player has unfortunately not escaped!", 3);
                Program.loader.currentScene.guiOptions.Clear();
                Program.loader.currentScene.guiOptions.Add(new Text("Unfortunately you have not escaped from the monster!", new Vector2(10, 10), 20, Color.BLACK));
                Thread.Sleep(300);
                enemyConcern.StartGUISequence();
            }
        }
    }
}
