using Raylib_cs;
using RPGConsole.Graphical;
using RPGConsole.Graphical.ScenesAvaliable;
using RPGConsole.InventoryItems;
using System;
using System.IO;
using System.Reflection.PortableExecutable;

namespace RPGConsole.InventoryBlock
{
    public class BlankMachinePart : Block
    {
        public string middleMachinePart = "Textures/blocks/machine_parts/blank_machine_part.png";
        public string rightMachinePart = "Textures/blocks/machine_parts/blank_machine_part_right.png";
        public string leftMachinePart = "Textures/blocks/machine_parts/blank_machine_part_left.png";
        public string upMachinePart = "Textures/blocks/machine_parts/blank_machine_part_up.png";
        public string downMachinePart = "Textures/blocks/machine_parts/blank_machine_part_down.png";

        public string others = "";
        public AssetLoader loaderAsset;

        public bool disabled = false;

        public int powerLevel = 0;

        public BlankMachinePart(string MachineBlockName, string middleMachinePart, string rightMachinePart, string leftMachinePart, string downMachinePart) : base(MachineBlockName, 0, 0, 5, Color.GRAY, "Textures/blocks/machine_parts/blank_machine_part.png", 1) {
            name = MachineBlockName;
            this.middleMachinePart = middleMachinePart;
            this.rightMachinePart = rightMachinePart;
            this.leftMachinePart = leftMachinePart;
            this.downMachinePart = downMachinePart;
            GeneralStartup();
        }

        public BlankMachinePart(string MachineBlockName):base(MachineBlockName, 0, 0, 5, Color.GRAY, 1)
        {
            name = MachineBlockName;
        }

        public BlankMachinePart() : base("Blank Machine Part", 0, 0, 5, Color.GRAY, 1)
        {

        }

        public void GeneralStartup()
        {
            if (loaderAsset == null)
            {
                MainScene sceneMain = Program.loader.currentScene as MainScene;
                if (sceneMain != null)
                {
                    loaderAsset = sceneMain.loaderAsset;
                }
            }
            UpdateBlock();
        }

        public void UpdateMachineBlock()
        {
            others = "";
            if (Program.gen.returnBlock(position.x + 1, position.y) is BlankMachinePart)
            {
                //we want to actually modify this!
                others += " right";
                BlankMachinePart part = Program.gen.returnBlock(position.x + 1, position.y) as BlankMachinePart;
                DemeritPowerLevel(part);
            }
            else if (Program.gen.returnBlock(position.x - 1, position.y) is BlankMachinePart)
            {
                others += " left";
                BlankMachinePart part = Program.gen.returnBlock(position.x - 1, position.y) as BlankMachinePart;
                DemeritPowerLevel(part);
            }
            else if (Program.gen.returnBlock(position.x, position.y + 1) is BlankMachinePart)
            {
                others += " down";
                BlankMachinePart part = Program.gen.returnBlock(position.x, position.y + 1) as BlankMachinePart;
                DemeritPowerLevel(part);
            }
            else if (Program.gen.returnBlock(position.x, position.y - 1) is BlankMachinePart)
            {
                others += " up";
                BlankMachinePart part = Program.gen.returnBlock(position.x, position.y - 1) as BlankMachinePart;
                DemeritPowerLevel(part);
            }
        }

        public void DemeritPowerLevel(BlankMachinePart machinePart)
        {
            if(machinePart != null)
            {
                if(machinePart.powerLevel > 0)
                {
                    machinePart.powerLevel = powerLevel - 1;
                }
            }
        }

        public override void UpdateBlock()
        {
            base.UpdateBlock();
            string[] splitString = others.Split(' ');
            for(int i = 0; i < splitString.Length; i++)
            {
                string splitStringChosen = splitString[i];
                switch (splitStringChosen.ToLower())
                {
                    case "right":
                        Raylib.DrawTexture(loaderAsset.textureLoad(rightMachinePart), (int)position.x, (int)position.y, Raylib_cs.Color.WHITE);
                        break;
                    case "left":
                        Raylib.DrawTexture(loaderAsset.textureLoad(leftMachinePart), (int)position.x, (int)position.y, Raylib_cs.Color.WHITE);
                        break;
                    case "down":
                        Raylib.DrawTexture(loaderAsset.textureLoad(downMachinePart), (int)position.x, (int)position.y, Raylib_cs.Color.WHITE);
                        break;
                    case "up":
                        Raylib.DrawTexture(loaderAsset.textureLoad(upMachinePart), (int)position.x, (int)position.y, Raylib_cs.Color.WHITE);
                        break;
                }
            }

            if(disabled == true)
            {
                Raylib.DrawTexture(loaderAsset.textureLoad("Textures/overlays/cross.png"), (int)position.x, (int)position.y, Raylib_cs.Color.WHITE);
            }
        }
    }
}
