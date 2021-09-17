using Raylib_cs;
using RPGConsole.InventoryBlock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace RPGConsole.Graphical.ScenesAvaliable
{
    public class MainScene : Scene
    {
        public Texture2D overlay;
        public AssetLoader loaderAsset;

        public MainScene() : base("Main Game Scene")
        {
            if(loaderAsset == null)
            {
                //setup the asset loader!
                loaderAsset = new AssetLoader();
            }
            //LoadTextures(true);
        }

        public bool gameActive = true;
        private bool craftingMode = false;
        private bool furnacingMode = false;
        private bool inventoryMode = false;
        public bool freezePlayer = false;

        public override void UpdateScene(Camera2D cam)
        {
            Raylib.SetExitKey(KeyboardKey.KEY_Q);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
            {
                Program.loader.LoadScene(Program.loader.getScene("Main Menu"));
            }
            if (Program.player != null) { cam.target = new System.Numerics.Vector2(Program.player.position.x * 64, Program.player.position.y * 64); }
            Raylib.BeginMode2D(cam);
            //draw the map!
            System.Numerics.Vector2 screen = new System.Numerics.Vector2(Raylib.GetScreenData().width, Raylib.GetScreenData().height);
            System.Numerics.Vector2 translation = Raylib.GetScreenToWorld2D(screen, cam);
            System.Numerics.Vector2 oppTrans = Raylib.GetScreenToWorld2D(System.Numerics.Vector2.Zero, cam);
            foreach (Block block in Program.gen.blockMap)
            {
                if (block != null)
                {
                    if ((block.position.x * 64) <= translation.X && (block.position.y * 64) <= translation.Y && (block.position.x * 64) >= oppTrans.X - 50 && (block.position.y * 64) >= oppTrans.Y - 20)
                    {
                        if (block.texture == string.Empty)
                        {
                            Color color = Color.WHITE;
                            switch (block.color)
                            {
                                case Block.Color.BROWN:
                                    color = new Color(127, 106, 79, 255);
                                    break;

                                case Block.Color.DARKBROWN:
                                    color = Color.DARKBROWN;
                                    break;

                                case Block.Color.GRAY:
                                    color = Color.GRAY;
                                    break;

                                case Block.Color.GREEN:
                                    color = Color.GREEN;
                                    break;

                                case Block.Color.LIGHTBLUE:
                                    color = Color.SKYBLUE;
                                    break;

                                case Block.Color.LIGHTBROWN:
                                    color = new Color(205, 133, 63, 255);
                                    break;

                                case Block.Color.LIGHTGRAY:
                                    color = Color.LIGHTGRAY;
                                    break;

                                case Block.Color.LIGHTGREEN:
                                    color = new Color(124, 252, 0, 255);
                                    break;

                                case Block.Color.ORANGE:
                                    color = Color.ORANGE;
                                    break;

                                case Block.Color.PASTEBROWN:
                                    color = Color.RAYWHITE;
                                    break;

                                case Block.Color.WHITESKINCOLOR:
                                    color = Color.BEIGE;
                                    break;
                                case Block.Color.SILVER:
                                    color = Color.DARKBLUE;
                                    break;
                                case Block.Color.BLACK:
                                    color = Color.BLACK;
                                    break;
                                default:
                                    //if a color gets added down the track we want to add one here
                                    if (Program.debugMode) { Console.WriteLine("Color is invalid!"); }
                                    break;
                            }
                            Raylib.DrawRectangle(((int)block.position.x * 64), ((int)block.position.y * 64), 64, 64, color);
                            //Raylib.DrawText(block.name + block.perlinvalue, (int)block.position.x * 64, (int)block.position.y * 64, 10, Color.BLACK);
                        }
                        else
                        {
                            //draw the texture attached!
                            Raylib.DrawTexture(block.textureInit(loaderAsset), (int)block.position.x * 64, (int)block.position.y * 64, Color.WHITE);
                        }
                        if(block.broken == true)
                        {
                            Raylib.DrawTexture(loaderAsset.textureLoad("Textures/overlays/brokenoverlay.png"), (int)block.position.x * 64, (int)block.position.y * 64, Color.WHITE);
                        }
                        block.UpdateBlock();
                    } 
                }
            }
            //Console.WriteLine("there are " + blocks + " in view!");
            //draw the players ontop
            //draw the individual player
            if (Program.player != null)
            {
                Vector2 newPosition = new Vector2(Program.player.position.x, Program.player.position.y);
                if (!freezePlayer)
                {
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
                    {
                        newPosition.y--;
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
                    {
                        newPosition.y++;
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
                    {
                        newPosition.x--;
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
                    {
                        newPosition.x++;
                    } 
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))
                {
                    if (craftingMode == false)
                    {
                        Program.player.InitiateCrafting();
                        freezePlayer = true;
                        craftingMode = true;
                        furnacingMode = false;
                    }
                    else
                    {
                        Program.loader.currentScene.guiOptions.Clear();
                        freezePlayer = false;
                        craftingMode = false;
                    }
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F))
                {
                    if (furnacingMode == false)
                    {
                        Program.player.InitiateFurnacing();
                        freezePlayer = true;
                        furnacingMode = true;
                        craftingMode = false;
                        inventoryMode = false;
                    }
                    else
                    {
                        Program.loader.currentScene.guiOptions.Clear();
                        freezePlayer = false;
                        furnacingMode = false;
                    }
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_I))
                {
                    if (inventoryMode == false)
                    {
                        Program.player.InitiateInventoryGUI();
                        freezePlayer = true;
                        inventoryMode = true;
                        craftingMode = false;
                        furnacingMode = false;
                    }
                    else
                    {
                        Program.loader.currentScene.guiOptions.Clear();
                        freezePlayer = false;
                        inventoryMode = false;
                    }
                }

                if (!freezePlayer)
                {
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        if (inventoryMode == false && furnacingMode == false && craftingMode == false)
                        {
                            //place item into the slot where the player is!
                            if (Program.gen.returnBlock(Program.player.position.x, Program.player.position.y).broken)
                            {
                                if (Program.player.equipItem as Air == null)
                                {
                                    if (Program.player.equipItem.typeMaterial == InventoryItems.entityType.BLOCK)
                                    {
                                        Program.gen.setBlock((int)Program.player.position.x, (int)Program.player.position.y, Program.player.equipItem as Block);
                                        Program.player.inv.removeItem(Program.player.equipItem);
                                        if (Program.player.equipItem.itemCount <= 0 || Program.player.equipItem == null)
                                        {
                                            Program.player.equipItem = new Air(); 
                                        }
                                    }
                                    else
                                    {
                                        if (Program.debugMode == true) { Console.WriteLine("Not a block to place!"); }
                                    } 
                                }
                            }
                            else
                            {
                                if (Program.debugMode == true) { Console.WriteLine("This block is not broken so unable to place!"); }
                            }
                            //LoadTextures(false);
                            //Program.gen.manualUpdate();
                        }
                    }

                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        //mine the block
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
                                    if (Program.debugMode == true) { Console.WriteLine("the block under you is already been broken!"); }
                                }
                            }
                            else
                            {
                                if (Program.debugMode == true) { Console.WriteLine("this block cannot be mined!"); }
                            }
                        }
                        else
                        {
                            if (Program.debugMode == true) { Console.WriteLine("for some reason that didnt work as the block doesnt exist!"); }
                        }
                        //Program.gen.manualUpdate();
                    }
                    Program.player.MovePlayer((int)newPosition.x, (int)newPosition.y); 
                }
                Raylib.DrawRectangle(((int)Program.player.position.x * 64) + 9, ((int)Program.player.position.y * 64) + 9, 45, 45, Color.WHITE);
            }
            Raylib.EndMode2D();
            if (gameActive)
            {
                for (int i = 0; i < Program.player.health; i++)
                {
                    Raylib.DrawRectangle((10 * i) + 10, 10, 10, 10, Color.RED);
                    if (i >= Program.player.health - 1)
                    {
                        Raylib.DrawText("Health " + Program.player.health, (10 * i) + 25, 10, 10, Color.RED);
                    }
                }

                if (Program.player.equipItem.name != "Air")
                {
                    if (Program.player.equipItem.textureInit(loaderAsset).width == 0)
                    {
                        Program.player.equipItem.textureInit(loaderAsset);
                        Console.WriteLine(Program.player.equipItem.texture);
                    }
                    Raylib.DrawTexture(Program.player.equipItem.textureInit(loaderAsset), (10 * Program.player.health) + 85, 10, Color.WHITE);
                    Raylib.DrawText(Program.player.equipItem.name, (10 * Program.player.health) + 90 + 64, 10, 10, Color.WHITE);
                }

                if (Program.player.health <= 0)
                {
                    gameActive = false;
                }
            }
            else
            {
                Program.player = null;
                Raylib.DrawText("Game Over", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, 30, Raylib_cs.Color.BLACK);
                Thread.Sleep(300);
                Console.WriteLine("game over!");
                Program.loader.LoadScene(Program.loader.getScene("Main Menu"));
            }
            Raylib.DrawRectangle(10, 5, 1 + loaderAsset.texturesCached.Count, 5, Color.BLACK);
            base.UpdateScene(cam);
        }
    }
}