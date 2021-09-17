using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class crafting : EmptyCommand
    {
        public crafting():base("Engage crafting mode", "crafting/cc", CommandType.DISCORDHYBRID) { }

        public override void RunCommand(string[] args, DiscordMessage message)
        {
            base.RunCommand(args, message);
            Program.player.InitiateCrafting();
        }
    }
}
