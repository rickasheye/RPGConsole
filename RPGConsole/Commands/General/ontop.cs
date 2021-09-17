using DSharpPlus.Entities;
using RPGConsole.InventoryBlock;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class ontop : EmptyCommand
    {
        public ontop():base("What block the player is ontop?", "ontop/ot", CommandType.DISCORDHYBRID) { }

        public override void RunCommand(string[] args, DiscordMessage message)
        {
            base.RunCommand(args, message);
            Block useableblock = Program.gen.returnBlock(Program.player.position.x, Program.player.position.y);
            if (useableblock != null)
            {
                Program.unit.AddConsoleItem("the player is currently sitting on block " + useableblock.name, message);
            }
            else
            {
                Program.unit.AddConsoleItem("somehow the player isnt sitting on anyblock!", message);
            }
        }
    }
}
