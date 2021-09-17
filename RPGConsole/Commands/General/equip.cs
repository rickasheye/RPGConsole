using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class equip : EmptyCommand
    {
        public equip():base("What item to equip", "equip", CommandType.DISCORDHYBRID) { }

        public override void RunCommand(string[] args, DiscordMessage message)
        {
            base.RunCommand(args, message);
            if (args.Length > 1) { Program.player.equipInventoryItem(args[1]); } else { Program.unit.AddConsoleItem("you didnt specify a item name!", message); }
        }
    }
}
