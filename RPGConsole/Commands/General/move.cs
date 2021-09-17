using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class move : EmptyCommand
    {
        public move():base("Where to move the player?", "right/left/up/down/move", CommandType.DISCORDHYBRID)
        {

        }

        public override void RunCommand(string[] args, DiscordMessage message)
        {
            base.RunCommand(args, message);
            string direction = args[0].ToLower();
            if (args != null && args.Length > 1)
            {
                if (args[0] != "up" || args[0] != "down" || args[0] != "left" || args[0] != "right")
                {
                    direction = args[1].ToLower();
                }
            }

            Vector2 newPos = new Vector2(Program.player.position.x, Program.player.position.y);
            switch (direction.ToLower())
            {
                case "up":
                    newPos.y++;
                    break;
                case "down":
                    newPos.y--;
                    break;
                case "left":
                    newPos.x--;
                    break;
                case "right":
                    newPos.x++;
                    break;
            }

            Program.player.MovePlayer((int)newPos.x, (int)newPos.y, message);
        }
    }
}
