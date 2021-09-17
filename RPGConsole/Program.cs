using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using Raylib_cs;
using RPGConsole.Commands;
using RPGConsole.Graphical;
using RPGConsole.Graphical.ScenesAvaliable;
using RPGConsole.InventoryBlock;
using RPGConsole.InventoryItems;
using RPGConsole.Profile;
using RPGConsole.Saving;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPGConsole
{
    class Program
    {
        public static ProfileSupport supportProfiles;
        public static Generator gen;

        public static bool cmdMode = false;

        public static Player player;
        public static SceneLoader loader;
        public static SystemSettings settings;

        public static ulong masteriddiscord = 745584917253193790;
        public static bool answermaster = false;
        //public static List<storedTexture> storedTextures = new List<storedTexture>();

        public static bool debugMode = true;
        public static ConsoleUnit unit = new ConsoleUnit();

        public static SaveFileManager manager = new SaveFileManager();

        public static bool discordBot = true;
        public static DateTime timeStart;

        static void Main(string[] args)
        {
            timeStart = Process.GetCurrentProcess().StartTime;
            Console.WriteLine("Started time at: " + timeStart.ToString());
            //get the discord bot ready if enabled!
            string settingsPath = Path.Combine(Environment.CurrentDirectory, "system.json");
            settings = SystemSettings.LoadSystemSettings(settings, settingsPath);

            string dirpath = Path.Combine(Environment.CurrentDirectory, "saves/");
            if (Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }

            if (debugMode == true) {
                unit.AddConsoleItem(new ConsoleItem(3, "Hello World... debug mode enabled!"));
            }
            supportProfiles = new ProfileSupport();
            if (gen == null)
            {
                if (discordBot == false) { gen = new Generator(512, 512); } else
                {
                    if (debugMode) { unit.AddConsoleItem("Worldwide generation has been disabled!"); }
                }
            }

            //cmdMode = settings.cmdMode;
            if (debugMode == true) { unit.AddConsoleItem(new ConsoleItem(3, "cmd mode is " + cmdMode + " but the config is read as " + settings.cmdMode)); }
            supportProfiles.InstallProfiles();


            unit.AddConsoleItem("Loaded " + manager.serverFiles.Count + " discord server files");

            player = new Player();
            if (supportProfiles.profiles.Count == 0)
            {
                Console.WriteLine("Enter in a username to use!");
                string username = "";
                if(discordBot == true)
                {
                    username = "rickasheye";
                }
                else
                {
                    username = Console.ReadLine();
                }
                Profile.Profile profile = new RPGConsole.Profile.Profile(username, supportProfiles);
                supportProfiles.CreateProfile(profile);
            }
            else
            {
                //skip this part and load the original one!
                Console.WriteLine("profile already created!");
            }

            if (cmdMode)
            {
                bool continueSave = false;
                while (!continueSave)
                {
                    Program.unit.AddConsoleItem("What would you like to do? and also welcome to RPG Console!");
                    Program.unit.AddConsoleItem("(new/create/cmon) create new save file!");
                    Program.unit.AddConsoleItem("(continue/old/play) see if there is a file to continue!");
                    Program.unit.AddConsoleItem("(opensaves/viewsaves/saves) see what files have been loaded!");
                    Program.unit.AddConsoleItem("or say what file you want to load by file name!");
                    if (debugMode == true) { Program.unit.AddConsoleItem("(skip) usually found from enabling debug mode"); }
                    for(int i = 0; i < manager.savefiles.Count; i++)
                    {
                        Program.unit.AddConsoleItem("(" + i + ") " + manager.savefiles[i].fileName);
                    }
                    string line = Console.ReadLine();
                    switch (line)
                    {
                        case "new":
                        case "create":
                        case "cmon":
                            if(manager.SaveFile() == true)
                            {
                                Program.unit.AddConsoleItem("Able to save the file and create a new one!");
                            }
                            else
                            {
                                Program.unit.AddConsoleItem("Something went wrong... please try again!");
                                Program.unit.AddConsoleItem("restarting...");
                                Console.Clear();
                            }
                            break;
                        case "continue":
                        case "old":
                        case "play":
                            if(manager.LoadFile(manager.getTopmostFile(), ref gen, ref player))
                            {
                                continueSave = true;
                            }
                            break;
                        case "opensaves":
                        case "viewsaves":
                        case "saves":
                            Console.Clear();
                            Program.unit.AddConsoleItem("use anyword to exit this prompt");
                            if (manager.savefiles != null || manager.savefiles.Count != -1)
                            {
                                for (int i = 0; i < manager.savefiles.Count - 1; i++)
                                {
                                    Program.unit.AddConsoleItem("(" + i + ") " + manager.savefiles[i].fileName);
                                }
                            }
                            else
                            {
                                Program.unit.AddConsoleItem("No files found...");
                            }
                            string collectableString = Console.ReadLine();
                            Console.Clear();
                            break;
                        case "skip":
                            if(debugMode == true)
                            {
                                continueSave = true;
                            }
                            break;
                        default:
                            bool aSave = false;
                            int lineSave = int.Parse(line);
                            for(int i = 0; i < manager.savefiles.Count; i++)
                            {
                                if (i == lineSave)
                                {
                                    unit.AddConsoleItem("Sucessfully picked " + line + " now loading!");
                                    aSave = true;
                                    break;
                                }
                            }

                            if(aSave == false)
                            {
                                unit.AddConsoleItem("Unfortunately that isnt a valid Command!");
                            }
                            else
                            {
                                if(manager.LoadFile(manager.savefiles[lineSave], ref gen, ref player))
                                {
                                    unit.AddConsoleItem("Yay we have found the file and loaded it!");
                                }
                                else
                                {
                                    unit.AddConsoleItem("That is an invalid file as it was unable to be loaded?");
                                }
                            }
                            break;
                    } 
                }
            }

            //player = supportProfiles.profiles[0].playerInstance;

            if (cmdMode)
            {
                Console.Clear();
                Console.WriteLine("Welcome to cmdRPG to get started please use the help command!");
                cmdInterpreter(player, gen);
                if (player.health <= 0)
                {
                    Console.WriteLine("game over restart app to try again!");
                }
            }
            else if(!cmdMode && !discordBot)
            {
                //Player is offset!
                //launch raylib mode
                loader = new SceneLoader();
                const int screenWidth = 800;
                const int screenHeight = 600;
                Raylib.InitWindow(screenWidth, screenHeight, "RPGConsole");

                Camera2D camera = new Camera2D();
                camera.offset = new System.Numerics.Vector2(screenWidth / 2, screenHeight / 2);
                camera.zoom = 1.0f;
                MainScene mainScene = new MainScene();
                MainMenu mainmenu = new MainMenu();
                MenuTest testmenu = new MenuTest();
                SavesMenu savesMeu = new SavesMenu();
                loader.AddScene(mainScene);
                loader.AddScene(mainmenu);
                loader.AddScene(testmenu);
                loader.AddScene(savesMeu);
                loader.LoadScene(mainScene);

                Raylib.SetTargetFPS(30);
                while (!Raylib.WindowShouldClose())
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.SKYBLUE);

                    if (loader.currentScene != null)
                    {
                        loader.currentScene.UpdateScene(camera);
                        if (debugMode == true) { Raylib.SetWindowTitle("RPGConsole - " + loader.currentScene.name); }
                    }
                    else
                    {
                        if (debugMode == true) { Raylib.SetWindowTitle("RPGConsole - No Scene Loaded!"); }
                    }
                    //draw hud etc
                    if (debugMode == true) { Raylib.DrawFPS(Raylib.GetMouseX() + 10, Raylib.GetMouseY() + 10);}
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_N))
                    {
                        if(debugMode == true)
                        {
                            debugMode = false;
                        }else if(debugMode == false)
                        {
                            debugMode = true;
                        }
                    }

                    if(debugMode == true)
                    {
                        for(int i = 0; i < unit.consoleItems.Count; i++)
                        {
                            if(unit.consoleItems.Count -1 > 30)
                            {
                                for(int m = 0; m < unit.consoleItems.Count - 3; m++)
                                {
                                    unit.consoleItems.RemoveAt(m);
                                }
                            }
                            Raylib.DrawText(unit.consoleItems[i].text, 10, (20 * i) + 15, 15, Color.BLACK);
                        }
                    }
                    Raylib.EndDrawing();
                }
                mainScene.loaderAsset.UnloadAll();
                if (debugMode == true) { Console.WriteLine("unloaded all textures!"); }

                Raylib.CloseWindow();
            }

            if (discordBot == true)
            {
                MainAsync().GetAwaiter().GetResult();
            }

            supportProfiles.ExitProfile();
            settings.SaveSettings(settingsPath);
            Environment.Exit(0);
        }

        public static DiscordClient discord;

        static async Task MainAsync()
        {
            discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = settings.DiscordToken,
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += async (s, e) =>
            {
                await MessageCreated(s, e);
            };

            /*discord.DisconnectAsync += async () =>
            {
                //convert the list to a string array
                List<string> array = new List<string>();
                for(int i = 0; i < unit.consoleItems.Count - 1; i++)
                {
                    array.Add(unit.consoleItems[i].text);
                }
                array.Add("END OF LOG");
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "applicationlog" + DateTime.Now.Second + DateTime.Now.Hour + DateTime.Now.Minute + ".log"));
            };*/

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        public static bool testMode = false;

        public static async Task MessageCreated(DiscordClient client, MessageCreateEventArgs message)
        {
            if(discordBot == true)
            {
                if(message.Guild == null && message.Author != client.CurrentUser)
                {
                    await message.Message.RespondAsync("Unfortunately this is an error, please use a guild with this bot installed!");
                    return;
                }

                if(message.Guild != null && message.Guild.Id != settings.DiscordPrivateServerName)
                {
                    if(debugMode == true)
                    {
                        return;
                        //await Task.CompletedTask;
                    }
                }

                if(answermaster == true)
                {
                    //answer master only
                    if(message.Author.Id != masteriddiscord)
                    {
                        return;
                    }
                }

                //load the discord user server profile.
                if(message.Author != discord.CurrentUser)
                {
                    player = manager.returnPlayerSaveFromDiscord(message.Guild.Id, message.Author.Id);
                    gen = manager.returnGenSaveFromDiscord(message.Guild.Id);

                    if(player.position.x < 0 && player.position.y < 0 && player.position.x > gen.sizeMapX && player.position.y > gen.sizeMapY)
                    {
                        player.position = new Vector2(0, 0);
                    }

                    if (message.Message.Content.StartsWith("!"))
                    {
                        string newMessage = message.Message.Content.Replace("!", "");
                        if (!player.craftingTable || !player.furnace)
                        {
                            executeCommand(newMessage, player, gen, message.Message);
                            await Task.CompletedTask;
                        }
                        else
                        {
                            if (player.craftingTable || player.crafting == true)
                            {
                                //generate a crafting table interface otherwise...
                                List<Crafting.CraftingRecipe> recipes = player.recipesCalc();
                                bool unfortunate = false;
                                if(recipes == null || recipes.Count < 0)
                                {
                                    unfortunate = true;
                                }

                                if (unfortunate == true)
                                {
                                    Program.unit.AddConsoleItem("Unfortunately you do not have any valid materials to craft with!", message.Message);
                                }
                                else
                                {
                                    for (int i = 0; i < recipes.Count; i++)
                                    {
                                        Program.unit.AddConsoleItem("(" + i + ") - " + recipes[i].output.name, message.Message);
                                    }
                                }

                                Program.unit.AddConsoleItem("give command (exit) to quit out of crafting prompt!", message.Message);
                                switch (newMessage)
                                {
                                    case "quit":
                                    case "exit":
                                    case "goodbye":
                                    case "bye":
                                    case "stop":
                                        player.craftingTable = false;
                                        Program.unit.AddConsoleItem("exited crafting...", message.Message);
                                        break;
                                }
                                int finalConvertedInteger = 0;
                                bool check = int.TryParse(newMessage, out finalConvertedInteger);
                                if (check == true)
                                {
                                    List<InventoryItem> item = Crafting.CraftingRecipe.convertToList(recipes[finalConvertedInteger]);
                                    foreach (InventoryItem itm in item)
                                    {
                                        player.inv.removeItem(itm);
                                    }
                                    player.inv.addItem(recipes[finalConvertedInteger].output);
                                }
                                else
                                {
                                    if (player.craftingTable == true)
                                    {
                                        Program.unit.AddConsoleItem("Unfortunately that number is incorrect!", message.Message);
                                    }
                                }
                            }
                            else if (player.furnace)
                            {
                                //generate a furnace interface...

                            }
                        }
                    }
                }
                manager.SaveFile();
            }
        }

        public static CommandRunManager managerRunCommand = new CommandRunManager();

        public static void executeCommand(string line, Player player, Generator gen, DiscordMessage messageOptional)
        {
            string[] argsM = line.Split(' ');
            managerRunCommand.FindCommand(argsM, messageOptional);
            if (Program.discordBot) { unit.AddConsoleItem("", "/", messageOptional); }
        }

        public static void executeCommand(string line, Player player, Generator gen)
        {
            executeCommand(line, player, gen, null);
        }

        public static void cmdInterpreter(Player player, Generator gen)
        {
            bool gameActive = true;
            Console.WriteLine("game has started to get used to the commands please type 'help'");
            while (gameActive)
            {
                string line = Console.ReadLine();
                executeCommand(line, player, gen);
                if(player.health <= 0)
                {
                    gameActive = false;
                }
            }
        }
    }
}