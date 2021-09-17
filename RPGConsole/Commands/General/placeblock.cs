using DSharpPlus.Entities;
using RPGConsole.InventoryBlock;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class placeblock : EmptyCommand
    {
        public placeblock():base("Place the block equipped in place in where the player is", "pb/placeblock/place", CommandType.DISCORDHYBRID) { }

        public override void RunCommand(string[] args, DiscordMessage message)
        {
            base.RunCommand(args, message);
            Block blockPlaced = Program.player.equipItem as Block;
            if (blockPlaced != null)
            {
                blockPlaced.broken = false;
                blockPlaced.strength = blockPlaced.originalStrength;
                Program.gen.setBlock((int)Program.player.position.x, (int)Program.player.position.y, blockPlaced);
                Program.player.inv.removeItem(blockPlaced);
                Program.unit.AddConsoleItem("The block " + blockPlaced.name + " was placed at " + Program.player.position.ToString(), message);
            }
            else
            {
                Program.unit.AddConsoleItem("the item equipped is not able to be placed!", message);
            }
        }
    }
}
