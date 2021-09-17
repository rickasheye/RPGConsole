using DSharpPlus.Entities;
using RPGConsole.InventoryBlock;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class mineblock : EmptyCommand
    {
        public mineblock():base("Mine the block under the player", "mb/break/breakblock/mineblock", CommandType.DISCORDHYBRID) { }

        public override void RunCommand(string[] args, DiscordMessage message)
        {
            base.RunCommand(args, message);
            //mine the block under the player!
            Block mineableBlock = Program.gen.returnBlock(Program.player.position.x, Program.player.position.y);
            if (mineableBlock != null)
            {
                if (mineableBlock.strength != -1)
                {
                    if (mineableBlock.broken == false)
                    {
                        mineableBlock.MineBlock(Program.player);
                    }
                    else
                    {
                        Program.unit.AddConsoleItem("the block under you is already been broken!", message);
                    }
                }
                else
                {
                    Program.unit.AddConsoleItem("this block cannot be mined!", message);
                }
            }
            else
            {
                Program.unit.AddConsoleItem("for some reason that didnt work as the block doesnt exist!", message);
            }
        }
    }
}
