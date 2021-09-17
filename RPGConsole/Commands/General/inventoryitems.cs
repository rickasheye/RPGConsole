using DSharpPlus.Entities;
using RPGConsole.InventoryItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class inventoryitems : EmptyCommand
    {
        public inventoryitems():base("Check your inventory items avaliable", "ii/inventory/checkinventory/ci/inventoryitems", CommandType.DISCORDHYBRID) { }

        public override void RunCommand(string[] args, DiscordMessage message)
        {
            base.RunCommand(args, message);
            Program.unit.AddConsoleItem("inventory items!", message);
            for (int i = 0; i < Program.player.inv.items.Count; i++)
            {
                InventoryItem item = Program.player.inv.items[i];
                if (item != null)
                {
                    Program.unit.AddConsoleItem("<" + item.itemCount + "> " + item.name, message);
                }
            }
        }
    }
}
