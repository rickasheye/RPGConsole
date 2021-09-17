using DSharpPlus.Entities;
using RPGConsole.InventoryBlock;
using RPGConsole.Saving;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class blocksaround : EmptyCommand
    {
        public blocksaround() : base("Checking the blocks around the player...", "blocksaround/fa/ba/findblocks/blockaround/findblock", CommandType.DISCORDHYBRID)
        {

        }

        public override void RunCommand(string[] args, DiscordMessage message)
        {
            base.RunCommand(args, message);
            //get the blocks around the player
            if (Program.player != null)
            {
                Program.unit.AddConsoleItem("Getting blocks in a 2 block radius", message);
                string positionValues = "";
                for (int y = ((int)Program.player.position.y - 2); y < Program.player.position.y + 2; y++)
                {
                    for (int x = ((int)Program.player.position.x - 2); x < Program.player.position.x + 2; x++)
                    {
                        Block block = Program.gen.returnBlock(x, y);
                        if (block != null)
                        {
                            if (!Program.discordBot)
                            {
                                if ((int)Program.player.position.x == x && (int)Program.player.position.y == y)
                                {
                                    string alternateplayername = "";
                                    if (Program.discordBot)
                                    {
                                        //convert player name to actual discord name
                                        ulong playerid = ulong.Parse(Program.player.name.Replace("Player: ", ""));
                                        string playerName = message.Channel.Guild.GetMemberAsync(playerid).Result.DisplayName;
                                        alternateplayername = "" + playerName;
                                    }
                                    else
                                    {
                                        alternateplayername = Program.player.name;
                                    }
                                    positionValues = positionValues + " " + Program.player.name;
                                }
                                else
                                {
                                    if (block.broken == false)
                                    {
                                        positionValues = positionValues + " " + block.name;
                                    }
                                    else
                                    {
                                        positionValues = positionValues + " <" + block.name + ">";
                                    }
                                }
                            }
                            else
                            {
                                DiscordServerFile file = Program.manager.returnSaveFile(message.Channel.Guild.Id);
                                int playerPosX = -1;
                                int playerPosY = -1;
                                List<Player> playersOnTop = new List<Player>();
                                for(int i = 0; i < file.playerSaves.Count; i++)
                                {
                                    Player player = file.playerSaves[i];
                                    if ((int)player.position.x == x && (int)player.position.y == y)
                                    {
                                        if (player.name == Program.player.name)
                                        {
                                            positionValues = positionValues + " YOU";
                                            playerPosX = (int)player.position.x;
                                            playerPosY = (int)player.position.y;
                                        }
                                        else
                                        {
                                            if (x != playerPosX && y != playerPosY)
                                            {
                                                ulong playerid = ulong.Parse(Program.player.name.Replace("Player: ", ""));
                                                string playerName = message.Channel.Guild.GetMemberAsync(playerid).Result.DisplayName;
                                                positionValues = positionValues + " " + playerName;
                                            }
                                            else
                                            {
                                                playersOnTop.Add(player);
                                            }
                                        } 
                                    }
                                }

                                //recite the players on top
                                Program.unit.AddConsoleItem("There are " + playersOnTop.Count + " players in your position", message);
                            }
                        }
                        else
                        {
                            positionValues = positionValues + " NULL";
                        }
                    }
                    positionValues = positionValues + "\n";
                }
                Program.unit.AddConsoleItem(positionValues, message);
            }
        }
    }
}
