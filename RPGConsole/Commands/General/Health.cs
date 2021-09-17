using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class Health : EmptyCommand
    {
        public Health():base("Checks the player health", "health/h/checkhealth/playerhealth", CommandType.DISCORDHYBRID) { }

        public override void RunCommand(string[] args, DiscordMessage message)
        {
            base.RunCommand(args, message);
            Program.unit.AddConsoleItem("Health: " + Program.player.health, message);
        }
    }
}
