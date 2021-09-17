using Newtonsoft.Json;
using RPGConsole.InventoryBlock;
using RPGConsole.InventoryItems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RPGConsole.Saving
{
    public class CombinedSaveFile
    {
        public List<Block> generator = new List<Block>();
        public List<Player> playerFiles = new List<Player>();
        public Player playerFile;

        public string fileName = "";

        public CombinedSaveFile(string path, Player playerSave, Generator genSave)
        {
            SaveFile(path, playerSave, genSave);
        }

        public CombinedSaveFile()
        {
            //nothing here...
            if (Program.debugMode) { Program.unit.AddConsoleItem("There is nothing here on this save file just yet..."); }
        }

        public void SaveFile(string path, Player playSave, Generator genSave)
        {
            //save in a path simmilar to "/saves"
            if (File.Exists(path))
            {
                if (playSave != null) { this.playerFile = playSave; } else
                {
                    if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately the player save option for this 'combinedsavefile' is disabled!"); }
                }
                foreach (Block moo in genSave.blockMap)
                {
                    if (moo == null || moo.name == "")
                    {
                        genSave.blockMap.Remove(moo);
                    }
                }
                this.generator = genSave.blockMap;
                string json = JsonConvert.SerializeObject(this);
                File.WriteAllText(path, json);
                Program.unit.AddConsoleItem("yes save file is saved!", 3);
            }
            else
            {
                Program.unit.AddConsoleItem("This file does not exist! " + path);
                string moddedPath = path.Replace(Path.GetFileName(path), "");
                if (Directory.Exists(moddedPath))
                {
                    Program.unit.AddConsoleItem("Directory exists?");
                    File.Create(path);
                }
                else
                {
                    Program.unit.AddConsoleItem("Created new saves directory...");
                    Directory.CreateDirectory(moddedPath);
                }
                SaveFile(path, playSave, genSave);
            }
        }

        public void LoadFile(string path)
        {
            if (File.Exists(path))
            {
                string jsonCode = File.ReadAllText(path);
                CombinedSaveFile json = JsonConvert.DeserializeObject<CombinedSaveFile>(jsonCode);
                generator = json.generator;
                if (json.playerFile != null) { playerFile = json.playerFile; }
                else { if (Program.debugMode) { Program.unit.AddConsoleItem("Unfortunately this savefile mode for the player is disabled!"); } }
                Program.unit.AddConsoleItem("Sucessfully read raw file!");
            }
            else
            {
                Program.unit.AddConsoleItem("This file unfortunately doesnt exist!");
            }
        }
    }
}
