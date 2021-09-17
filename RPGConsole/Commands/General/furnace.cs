using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class furnace : EmptyCommand
    {
        public furnace():base("Enagage furnacing interface", "furnance", CommandType.DISCORDHYBRID) { }

        public override void RunCommand(string[] args, DiscordMessage message)
        {
            base.RunCommand(args, message);
            Program.player.InitiateFurnacing();
        }
    }
}
