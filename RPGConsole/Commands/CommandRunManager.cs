using DSharpPlus.Entities;
using RPGConsole.Commands.General;
using RPGConsole.Commands.General.ConsoleExclusive;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands
{
    public enum CommandType
    {
        DEBUG, NORMAL, DISCORD, DISCORDHYBRID
    }

    public class CommandRunManager
    {
        //run a command
        public List<EmptyCommand> emptyCommands = new List<EmptyCommand>();

        public CommandRunManager()
        {
            //load up the commands

            //Normal commands
            emptyCommands.Add(new ClearConsole());
            emptyCommands.Add(new exit());

            //Discord Hybrid commands
            emptyCommands.Add(new blocksaround());
            emptyCommands.Add(new crafting());
            emptyCommands.Add(new equip());
            emptyCommands.Add(new furnace());
            emptyCommands.Add(new Health());
            emptyCommands.Add(new inventoryitems());
            emptyCommands.Add(new mineblock());
            emptyCommands.Add(new move());
            emptyCommands.Add(new ontop());
            emptyCommands.Add(new placeblock());
            emptyCommands.Add(new playerposition());
        }

        public void FindCommand(string[] args, DiscordMessage message)
        {
            if (args[0].Contains("!"))
            {
                args[0] = args[0].Replace("!", "");
            }
            if (!args[0].Contains("help")){
                //Check if the command exists first
                bool commandExist = false;
                //if (Program.debugMode) { message.RespondAsync("Message commands " + args[0]); }
                int chosenCommand = 0;
                for(int m = 0; m < emptyCommands.Count - 1; m++)
                {
                    EmptyCommand commandEmpty = emptyCommands[m];
                    if(equalSplitCommand(commandEmpty.syntax, args[0].ToLower()) || args[0] == "debug")
                    {
                        commandExist = true;
                        //if (Program.debugMode) { message.RespondAsync(commandEmpty.syntax); }
                        chosenCommand = m;
                        break;
                    }
                }
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine("command exists: " + commandExist);
                //Console.ResetColor();
                if (commandExist == true)
                {
                    for (int i = 0; i < emptyCommands.Count - 1; i++)
                    {
                        //Program.unit.AddConsoleItem("Found command");
                        EmptyCommand commandEmpty = emptyCommands[i];
                        if (commandEmpty.typeCommand == CommandType.DEBUG)
                        {
                            if (Program.debugMode) { Program.unit.AddConsoleItem("Command is debug enabled"); }
                            if (args[0] == "debug")
                            {
                                if (Program.debugMode) { Program.unit.AddConsoleItem("Command contains debug inside"); }
                                if (Program.debugMode)
                                {
                                    Program.unit.AddConsoleItem("is Debug mode enabled!");
                                    if (args[1] == commandEmpty.syntax)
                                    {
                                        Program.unit.AddConsoleItem("Command contains the syntax");
                                        if (message == null) { commandEmpty.RunCommand(args); }
                                        else
                                        {
                                            commandEmpty.RunCommand(args, message);
                                        }
                                    }
                                }
                                else
                                {
                                    Program.unit.AddConsoleItem("Unfortunately these commands are only avaliable in debug mode...");
                                }
                            }
                        }
                        else
                        {
                            if (i == chosenCommand)
                            {
                                //then its a non debug command
                                if (commandEmpty.typeCommand == CommandType.NORMAL || commandEmpty.typeCommand == CommandType.DISCORDHYBRID)
                                {
                                    if (Program.discordBot && commandEmpty.typeCommand == CommandType.DISCORDHYBRID)
                                    {
                                        if (message == null)
                                        {
                                            commandEmpty.RunCommand(args);
                                        }
                                        else
                                        {
                                            commandEmpty.RunCommand(args, message);
                                        }
                                    }
                                    else
                                    {
                                        commandEmpty.RunCommand(args, null);
                                    }
                                }
                                else if (commandEmpty.typeCommand == CommandType.DISCORD)
                                {
                                    if (Program.discordBot)
                                    {
                                        if (message == null) { commandEmpty.RunCommand(args); }
                                        else
                                        {
                                            commandEmpty.RunCommand(args, message);
                                        }
                                    }
                                    else
                                    {
                                        Program.unit.AddConsoleItem("Unfortunately you are not engaged into discord mode...");
                                    }
                                } 
                            }
                        }
                    }
                }
                else
                {
                    Program.unit.AddConsoleItem("That command doesnt exist it seems!");
                }
            }
            else
            {
                if (Program.discordBot)
                {
                    HelpCommand(args[1], message);
                }
            }
        }

        public bool equalSplitCommand(string split, string compare)
        {
            //if (Program.debugMode) { Program.unit.AddConsoleItem("User has entered: " + split); }
            string[] splitComamnd = split.Split('/');
            for(int i = 0; i < splitComamnd.Length; i++)
            {
                if(splitComamnd[i].ToLower() == compare.ToLower())
                {
                    //Console.ForegroundColor = ConsoleColor.Blue;
                    //Console.WriteLine("true");
                    //Console.ResetColor();
                    return true;
                }
            }
            return false;
        }

        public void HelpCommand(string type, DiscordMessage message)
        {
            //parse the type
            CommandType cmdtype = CommandType.NORMAL;
            if (Program.discordBot) { cmdtype = CommandType.DISCORDHYBRID; }
            if(type.ToLower() == null || type.ToLower() == "")
            {
                string application = "";
                if (Program.debugMode)
                {
                    application += "DEBUG/";
                }

                if (Program.discordBot)
                {
                    application += "DISCORDHYBRID/DISCORD";
                }
                else
                {
                    application += "NORMAL/DISCORDHYBRID";
                }
                Program.unit.AddConsoleItem("Unfortunately you did not select any one of the modes " + application, message);
            }
            switch (type.ToLower())
            {
                case "normal":
                    if (!Program.discordBot)
                    {
                        cmdtype = CommandType.NORMAL;
                    }
                    else
                    {
                        Program.unit.AddConsoleItem("Unfortunately you cannot use these commands as they're not supported in this mode of play...", message);
                    }
                    break;
                case "debug":
                    if (Program.debugMode) { if (Program.discordBot) { cmdtype = CommandType.DEBUG; }
                        else
                        {
                            Program.unit.AddConsoleItem("Unfortunately most of these commands are only avalible in Discord mode");
                        }
                    } else
                    {
                        if (Program.discordBot)
                        {
                            Program.unit.AddConsoleItem("Debug mode is disabled, so these commands are not allowed!", message);
                        }
                    }
                    break;
                case "discord":
                    cmdtype = CommandType.DISCORD;
                    break;
                case "discordhybrid":
                    cmdtype = CommandType.DISCORDHYBRID;
                    break;
                default:
                    if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately this user didnt add a mode select"); }
                    break;
            }
            List<EmptyCommand> foundCommands = emptyCommands.FindAll(t => t.typeCommand == cmdtype);
            Program.unit.AddConsoleItem("Command Type: " + cmdtype.ToString(), message);
            if(foundCommands.ToArray().Length <= 0) { Program.unit.AddConsoleItem("Unfortunately no commands were found..."); }
            for(int i = 0; i < foundCommands.ToArray().Length; i++)
            {
                Program.unit.AddConsoleItem(foundCommands[i].commandName + " - (" + foundCommands[i].syntax + ")", message);
            }
        }
    }
}
