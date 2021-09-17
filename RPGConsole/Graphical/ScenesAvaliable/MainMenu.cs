using Raylib_cs;
using RPGConsole.Graphical.MenuItems;
using RPGConsole.Graphical.MenuItems.KeyboardOnlyItems;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RPGConsole.Graphical.ScenesAvaliable
{
    public class MainMenu : Scene
    {
        public MainMenu():base("Main Menu") {
        }

        Text text;
        ButtonPlay playbutton;
        ButtonCreateNewSave newSave;
        ButtonQuit quitbutton;
        EmptyContainer container;
        public Dictionary<float[,], int> textureIndexes = new Dictionary<float[,], int>();
        Model startingModel = new Model();
        Camera3D ThreeDCam = new Camera3D();

        public override void LoadScene()
        {
            base.LoadScene();
            text = new Text("RPGConsole", new Vector2(10, 10), 40, Raylib_cs.Color.BLACK);
            playbutton = new ButtonPlay();
            newSave = new ButtonCreateNewSave();
            quitbutton = new ButtonQuit();
            container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
            guiOptions.Add(text);
            guiOptions.Add(playbutton);
            guiOptions.Add(newSave);
            guiOptions.Add(quitbutton);
            container.children.AddRange(guiOptions);
            guiOptions.Add(container);
            ThreeDCam.position = new Vector3(14, 14, 16);
            ThreeDCam.target = new Vector3(0, 0, 0);
            ThreeDCam.up = new Vector3(0, 1, 0);
            ThreeDCam.fovy = 45;
            Raylib.SetCameraMode(ThreeDCam, CameraMode.CAMERA_ORBITAL);

            startingModel = Raylib.LoadModel("Models/mainMenuItem.obj");
            unsafe
            {
                Material* materials = (Material*)startingModel.materials.ToPointer();
                MaterialMap* maps = (MaterialMap*)materials[0].maps.ToPointer();
                maps[(int)MaterialMapType.MAP_ALBEDO].texture = Raylib.LoadTexture("Textures/models/menuitem.png");
            }
        }

        public override void UpdateScene(Camera2D cam)
        {
            base.UpdateScene(cam);
            Raylib.BeginMode3D(ThreeDCam);
            //Raylib.DrawCube(new Vector3(0, 0, 0), 3, 3, 3, Color.WHITE);
            Raylib.DrawModel(startingModel, new Vector3(0, 0, 0), 0.1f, Color.WHITE);
            Raylib.EndMode3D();
            Raylib.UpdateCamera(ref ThreeDCam);
        }
    }

    public class ButtonPlay : KeyboardAdjustedButton
    {
        //Continue on the last save!!!
        public ButtonPlay():base("Continue", new Vector2(40, 80), new Vector2(10, 60)) {
            if(Program.manager.savefiles.Count <= 0)
            {
                name = "Load new save file!";
            }
        }

        public override void Triggerable()
        {
            base.Triggerable();
            if (Program.manager.savefiles.Count > 0)
            {
                if (Program.manager.LoadFile(Program.manager.getTopmostFile(), ref Program.gen, ref Program.player))
                {
                    Program.unit.AddConsoleItem("Sucessfully loaded the save file!");
                }
                else
                {
                    Program.unit.AddConsoleItem("Unfortunately unable to load save file!");
                    if (Program.manager.SaveFile())
                    {
                        Program.unit.AddConsoleItem("Successfully saved your file!");
                        if (Program.manager.LoadFile(Program.manager.savefiles[Program.manager.savefiles.Count - 1], ref Program.gen, ref Program.player))
                        {
                            Program.unit.AddConsoleItem("Sucessfully loaded your file!");
                        }
                        else
                        {
                            Program.unit.AddConsoleItem("Unfortunately your file was unable to be loaded!");
                        }
                    }
                    else
                    {
                        Program.unit.AddConsoleItem("Unfortunately your file has not saved correctly!");
                    }
                }
            }
            else
            {
                //create a new file!@
                Program.manager.SaveFile();
                Triggerable();
            }
            Program.loader.LoadScene(Program.loader.getScene("Main Game Scene"));
        }
    }

    public class ButtonCreateNewSave : KeyboardAdjustedButton
    {
        //Create a new save and play that!
        public ButtonCreateNewSave() : base("Create new save! and play!", new Vector2(40, 80), new Vector2(10, 120)) {
            if (Program.manager.savefiles.Count <= 0)
            {
                disabled = true;
            }
        }

        public override void Triggerable()
        {
            base.Triggerable();
            if (Program.manager.SaveFile())
            {
                Program.unit.AddConsoleItem("Successfully saved your file!");
                if(Program.manager.LoadFile(Program.manager.savefiles[Program.manager.savefiles.Count - 1], ref Program.gen, ref Program.player))
                {
                    Program.unit.AddConsoleItem("Sucessfully loaded your file!");
                }
                else
                {
                    Program.unit.AddConsoleItem("Unfortunately your file was unable to be loaded!");
                }
            }
            else
            {
                Program.unit.AddConsoleItem("Unfortunately your file has not saved correctly!");
            }
        }
    }

    public class ButtonQuit : KeyboardAdjustedButton
    {
        public ButtonQuit() : base("Quit", new Vector2(40, 80), new Vector2(10, 180)) { }

        public override void Triggerable()
        {
            base.Triggerable();
            Environment.Exit(0);
        }

        public override void DrawObject()
        {
            base.DrawObject();
        }
    }
}
