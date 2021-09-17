using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class playerposition : EmptyCommand
    {
        public playerposition():base("Where the player is on the map.", "playerposition/pp/playerpos", CommandType.DISCORDHYBRID) {  }

        public override void RunCommand(string[] args, DiscordMessage message)
        {
            base.RunCommand(args, message);
            message.RespondAsync("Player is at: " + Program.player.position.ToString());
        }
    }
}
