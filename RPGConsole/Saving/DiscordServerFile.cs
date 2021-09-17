using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using DSharpPlus;
using DSharpPlus.Entities;

namespace RPGConsole.Saving
{
    public class DiscordServerFile
    {
        public ulong id = 0;
        public CombinedSaveFile saveFile;
        public List<Player> playerSaves = new List<Player>();

        public DiscordServerFile(ulong serverid, CombinedSaveFile saveFile, List<Player> players)
        {
            //get guild
            id = serverid;
            if (saveFile != null) { this.saveFile = saveFile; } else
            {
                this.saveFile = new CombinedSaveFile();
            }

            if (players != null) { this.playerSaves = players; } else
            {
                this.playerSaves = new List<Player>();
            }
            SaveFile();
        }

        public DiscordServerFile(ulong serverid, CombinedSaveFile saveFile)
        {
            id = serverid;
            if (saveFile != null) { this.saveFile = saveFile; } else
            {
                saveFile = new CombinedSaveFile();
            }
            this.playerSaves = new List<Player>();
            if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately unable to make this work since there is no player profiles avaliable!"); }
            SaveFile();
        }

        public DiscordServerFile(ulong serverid)
        {
            id = serverid;
            this.saveFile = new CombinedSaveFile();
            this.playerSaves = new List<Player>();
            if (Program.debugMode) { Program.unit.AddConsoleItem("This method isnt recommended as it can cause problems!"); }
            SaveFile();
        }

        public DiscordServerFile()
        {
            this.saveFile = new CombinedSaveFile();
            this.playerSaves = new List<Player>();
            if (Program.debugMode) { Program.unit.AddConsoleItem("This method should only be used to load a file into!"); }
            SaveFile();
        }

        public void SaveFile()
        {
            string savesPath = Path.Combine(Environment.CurrentDirectory, "saves/");
            string storeablePath = Path.Combine(savesPath, "" + id);
            if (Program.debugMode) { Program.unit.AddConsoleItem(storeablePath); }

            if (!Directory.Exists(savesPath))
            {
                Directory.CreateDirectory(savesPath);
            }

            if (Directory.Exists(storeablePath))
            {
                //the directory exists

                //Save the safe file generator etc.
                string convSAVEJSON = JsonConvert.SerializeObject(saveFile);
                string SaveJSON = Path.Combine(storeablePath, "saveFile.json");

                if (!File.Exists(SaveJSON)) { File.Create(SaveJSON); }
                if (File.Exists(SaveJSON))
                {
                    if (Program.debugMode) { Program.unit.AddConsoleItem("Save file for " + id + " has been saved!"); }
                    Generator gen = new Generator(128, 128);
                    saveFile.SaveFile(SaveJSON, null, gen);
                }

                string convPLAYERSAVES = JsonConvert.SerializeObject(playerSaves);
                string PlayerJSON = Path.Combine(storeablePath, "playerSavesFile.json");

                if (!File.Exists(PlayerJSON)) { File.Create(PlayerJSON); }
                if (File.Exists(PlayerJSON))
                {
                    //check for duplicates
                    for(int i = 0; i < playerSaves.Count - 1; i++)
                    {
                        for(int m = 0; m < playerSaves.Count - 1; m++)
                        {
                            if(i != m)
                            {
                                if(playerSaves[i] == playerSaves[m])
                                {
                                    if (Program.debugMode)
                                    {
                                        Program.unit.AddConsoleItem("A duplicate profile has been found! removing...");
                                        playerSaves.RemoveAt(m);
                                    }
                                }
                            }
                        }
                    }
                    if (Program.debugMode) { Program.unit.AddConsoleItem("Player save files for " + id + " have been saved!"); }
                    File.WriteAllText(PlayerJSON, convPLAYERSAVES);
                }
            }
            else
            {
                Directory.CreateDirectory(storeablePath);
                SaveFile();
            }
        }

        public void LoadFile()
        {
            string savesPath = Path.Combine(Environment.CurrentDirectory, "/saves/");
            string storeablePath = Path.Combine(savesPath, "" + id);

            if (Directory.Exists(storeablePath))
            {
                //the directory exists

                //Save the safe file generator etc.
                //string convSAVEJSON = JsonConvert.SerializeObject(saveFile);
                string SaveJSON = Path.Combine(storeablePath, "saveFile.json");

                if (File.Exists(SaveJSON))
                {
                    if (Program.debugMode) { Program.unit.AddConsoleItem("Save file for " + id + " has been loaded!"); }
                    saveFile = JsonConvert.DeserializeObject<CombinedSaveFile>(File.ReadAllText(SaveJSON));
                }

                //string convPLAYERSAVES = JsonConvert.SerializeObject(playerSaves);
                string PlayerJSON = Path.Combine(storeablePath, "playerSavesFile.json");

                if (!File.Exists(PlayerJSON)) { File.Create(PlayerJSON); }
                if (File.Exists(PlayerJSON))
                {
                    if (Program.debugMode) { Program.unit.AddConsoleItem("Player save files for " + id + " have been saved!"); }
                    playerSaves = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(PlayerJSON));
                }
            }
            else
            {
                Directory.CreateDirectory(storeablePath);
                SaveFile();
            }
        }

        public Player returnSave(ulong id)
        {
            bool returnedFile = false;
            for(int i = 0; i < playerSaves.Count -1; i++)
            {
                Player playerSave = playerSaves[i];
                if (playerSave.discordid == id)
                {
                    returnedFile = true;
                    return playerSave;
                }
            }

            if(returnedFile == false)
            {
                //file doesnt seem to be recorded?
                if (Program.debugMode) { Program.unit.AddConsoleItem("Player file doesnt seem to be recorded recording..."); }
                Player newPlayer = new Player();
                newPlayer.discordid = id;
                newPlayer.name = "Player: " + id;
                playerSaves.Add(newPlayer);
                return returnSave(id);
            }
            return null;
        }
    }
}
