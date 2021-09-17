using DSharpPlus.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using RPGConsole.InventoryBlock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RPGConsole.Saving
{
    public class SaveFileManager
    {
        public List<CombinedSaveFile> savefiles = new List<CombinedSaveFile>();
        public List<DiscordServerFile> serverFiles = new List<DiscordServerFile>();

        public SaveFileManager()
        {
            //save the files
            if (Program.debugMode == true) { Program.unit.AddConsoleItem("Save File Manager loaded!"); }
            if(!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "saves/")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "saves/"));
            }

            if (!Program.discordBot)
            {
                foreach (string m in Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "saves/")))
                {
                    if (File.Exists(m))
                    {
                        //then is file
                        CombinedSaveFile saveFile = new CombinedSaveFile();
                        saveFile.LoadFile(m);
                        foreach (Block moo in saveFile.generator)
                        {
                            if (moo == null || moo.name == "")
                            {
                                saveFile.generator.Remove(moo);
                            }
                        }
                    }
                }
            }
            else
            {
                //loading server files for discord
                foreach (string m in Directory.EnumerateDirectories(Path.Combine(Environment.CurrentDirectory, "saves/")))
                {
                    Program.unit.AddConsoleItem("Found: " + m);
                    bool isValid = false;
                    if (File.Exists(Path.Combine(m, "saveFile.json")))
                    {
                        if (File.Exists(Path.Combine(m, "playerSavesFile.json")))
                        {
                            isValid = true;
                        }
                        else
                        {
                            if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately... playerSavesFile.json is missing in " + m + " so it was unable to be loaded!"); }
                        }
                    }
                    else {
                        if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately... saveFile.json is missing in " + m + " so it was unable to be loaded!"); }
                    }

                    if(isValid == true)
                    {
                        //load the save files
                        string id = Path.GetDirectoryName(m);
                        DiscordServerFile serverFile = new DiscordServerFile(ulong.Parse(id));
                        serverFile.LoadFile();
                    }
                    else
                    {
                        //Unable to be loaded
                        if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately... this folder for a server was unable to be loaded!"); }
                    }
                }
            }
        }

        public bool SaveFiles(CombinedSaveFile saveFile)
        {
            if (Program.debugMode)
            {
                Program.unit.AddConsoleItem("Saving file...");
            }

            if (!Program.discordBot)
            {
                if (Program.debugMode) { Program.unit.AddConsoleItem("Saving individual player profile..."); }
                if (FileExists(saveFile))
                {
                    return LoadFile(saveFile, ref Program.gen, ref Program.player);
                }
                else
                {
                    Random rand = new Random();
                    saveFile.fileName = "saveFile" + rand.Next(5000) + ".json";
                    saveFile.SaveFile(Path.Combine(Environment.CurrentDirectory, "saves/" + saveFile.fileName), Program.player, Program.gen);
                    Program.unit.AddConsoleItem("Saved file successfully", 3);
                    return true;
                }
            }
            else
            {
                //check and save for the current discord serve
                if (Program.debugMode) { Program.unit.AddConsoleItem("Saving all profiles attached to discord profiles."); }
                if(serverFiles != null)
                {
                    //if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately there is no save file method just yet!"); }
                    //save the server-side files
                    for(int i = 0; i < serverFiles.Count; i++)
                    {
                        DiscordServerFile file = serverFiles[i];
                        if (file.id != 0) { file.SaveFile(); }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool SaveFile()
        {
            return SaveFiles(new CombinedSaveFile());
        }

        public CombinedSaveFile getTopmostFile()
        {
            return savefiles[0]; 
        }

        public Player returnPlayerSaveFromDiscord(ulong serverID, ulong id)
        {
            if (Program.discordBot)
            {
                bool returnedFile = false;
                for (int i = 0; i < serverFiles.Count; i++)
                {
                    DiscordServerFile file = serverFiles[i];
                    if (file.id == serverID)
                    {
                        returnedFile = true;
                        return file.returnSave(id);
                    }
                }
                if (!returnedFile)
                {
                    //file doesnt exist so create one i guess??
                    if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately, this server file doesnt exist so creating?"); }
                    DiscordServerFile file = new DiscordServerFile(serverID);
                    serverFiles.Add(file);
                    return returnPlayerSaveFromDiscord(serverID, id);
                }
                SaveFiles(null);
            }
            else
            {
                if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately, this is unsupported as it was intended to grab discord server files"); }
            }
            return null;
        }

        public Generator returnGenSaveFromDiscord(ulong serverID)
        {
            if (Program.discordBot)
            {
                //if state is discord bot please run
                for(int i = 0; i < serverFiles.Count; i++)
                {
                    DiscordServerFile file = serverFiles[i];
                    if(file.id == serverID)
                    {
                        Generator gen = new Generator(file.saveFile.generator);
                        return gen;
                    }
                }
            }
            else
            {
                if (Program.debugMode) { Program.unit.AddConsoleItem("This function is not allowed to be executed in outside of discord mode!!!"); }
            }
            return null;
        }

        public DiscordServerFile returnSaveFile(ulong serverID)
        {
            for(int i = 0; i < serverFiles.Count; i++)
            {
                DiscordServerFile fileChosen = serverFiles[i];
                if(fileChosen.id == serverID)
                {
                    return fileChosen;
                }
            }
            return null;
        }

        public bool serverSaved(ulong serverid)
        {
            bool notfound = false;
            for(int i = 0; i < serverFiles.Count - 1; i++)
            {
                DiscordServerFile serverFile = serverFiles[i];
                if(serverFile.id == serverid)
                {
                    notfound = true;
                    string serverPath = Path.Combine(Environment.CurrentDirectory, "saves/", serverFile.saveFile.fileName, "saveFile.json");
                    if (File.Exists(serverPath))
                    {
                        CombinedSaveFile savefile = JsonConvert.DeserializeObject<CombinedSaveFile>(serverPath);
                        CombinedSaveFile newFile = new CombinedSaveFile();
                        if(savefile != newFile)
                        {
                            return true;
                        }
                        else
                        {
                            if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately the saved file is a empty representation of the object"); }
                        }
                    }
                    else
                    {
                        if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately the server file is missing?"); }
                    }
                }
            }

            if(notfound == false)
            {
                //Unfortunately that server has not been found in the registry at all...
                if (Program.debugMode) { Program.unit.AddConsoleItem("That server file has not been added to the registry at all!!!"); }
            }
            return false;
        }

        public bool userSaved(ulong id, ulong serverID)
        {
            bool found = false;
            bool playerfound = false;
            for(int i = 0; i < serverFiles.Count -1; i++)
            {
                DiscordServerFile serverFIle = serverFiles[i];
                if(serverFIle.id == serverID)
                {
                    found = true;
                    for(int m = 0; m < serverFIle.playerSaves.Count - 1; i++)
                    {
                        Player playerFile = serverFIle.playerSaves[m];
                        if(playerFile.discordid == id)
                        {
                            playerfound = true;
                            //saved here but check if its actually saved in the files.
                            string filePath = Path.Combine(Environment.CurrentDirectory, "saves/", serverFIle.saveFile.fileName, "playerSavesFile.json");
                            if (File.Exists(filePath))
                            {
                                bool unregisteredPlayerTOSave = false;
                                //file exists thats a good start
                                List<Player> player = JsonConvert.DeserializeObject<List<Player>>(filePath);
                                for(int y = 0; y < player.Count - 1; y++)
                                {
                                    Player playerm = player[y];
                                    if(playerm.discordid == id)
                                    {
                                        unregisteredPlayerTOSave = true;
                                        return true;
                                    }
                                }

                                if(unregisteredPlayerTOSave == false)
                                {
                                    if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately that player hasnt been registered to the save file just yet!!!"); }
                                }
                            }
                            else
                            {
                                if (Program.debugMode) { Program.unit.AddConsoleItem("player save file doesnt exist at all unfortunately"); }
                                break;
                            }
                        }
                    }
                }
            }

            if(found == false)
            {
                if (Program.debugMode) { Program.unit.AddConsoleItem("Server save hasnt been found at all which means it isnt registered!"); }
            }

            if(playerfound == false)
            {
                if (Program.debugMode) { Program.unit.AddConsoleItem("Player save hasnt been found at all which means it isnt registered!"); }
            }
            return false;
        }

        public void DeleteFile(CombinedSaveFile saveFile)
        {
            if (!FileExists(saveFile))
            {
                Program.unit.AddConsoleItem("Unfortunately this save file doesnt exist?", 3);
            }
            else
            {
                string path = Path.Combine(Environment.CurrentDirectory, "saves/saveFile" + saveFile.fileName);
                if (File.Exists(path))
                {
                    Program.unit.AddConsoleItem("OMG this actually file exists? yay! now lets delete it " + path, 3);
                    File.Delete(path);
                }
                else
                {
                    Program.unit.AddConsoleItem("This file doesnt exist great! " + path, 3);
                }
                //actually do it my way which is taxing on cpu cycles but just do it
                for(int i = 0; i < savefiles.Count; i++)
                {
                    if(savefiles[i] == saveFile)
                    {
                        savefiles.RemoveAt(i);
                    }
                }
                Program.unit.AddConsoleItem("Finished the removal cycle now this is deleted!");
                //actually remove the file located for it or well atleast above this part!
            }
        }

        public bool LoadFile(CombinedSaveFile file, ref Generator genInstance, ref Player playerInstance)
        {
            if (FileExists(file))
            {
                Console.WriteLine(file.fileName);
                string path = Path.Combine(Environment.CurrentDirectory, "saves/" + file.fileName);
                if (File.Exists(path))
                {
                    Program.gen.blockMap = file.generator;
                    Program.player = file.playerFile;
                    Program.unit.AddConsoleItem("Sucessfully loaded save file!");
                    return true;
                    //bc under here is unacessable!
                }
                else
                {
                    Program.unit.AddConsoleItem("Unfortunately this file failed to exist? so recreating?");
                    SaveFiles(file);
                    return LoadFile(file, ref genInstance, ref playerInstance);
                }
                //unreachable code here
                return false;
            }
            else
            {
                Program.unit.AddConsoleItem("Unfortunately this file doesnt exist in the directory? but creating a new one anyway!");
                SaveFiles(file);
                return LoadFile(file, ref genInstance, ref playerInstance);
            }
        }

        public bool FileExists(CombinedSaveFile saveFile)
        {
            if (savefiles.Count > 0)
            {
                for (int i = 0; i < savefiles.Count - 1; i++)
                {
                    if (savefiles[i] == saveFile)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
    }
}
