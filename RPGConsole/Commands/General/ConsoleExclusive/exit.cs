using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class exit : EmptyCommand
    {
        public exit():base("exit this instance...", "exit", CommandType.NORMAL)
        {

        }

        public override void RunCommand(string[] args, DiscordMessage message)
        {
            base.RunCommand(args, message);
            if (Program.discordBot)
            {
                Program.unit.AddConsoleItem("Someone tried to execute this through discord?");
                Program.unit.AddConsoleItem("Unfortunately this command is not allowed", message);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
