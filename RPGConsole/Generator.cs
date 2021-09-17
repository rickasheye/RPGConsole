using Raylib_cs;
using RPGConsole.InventoryBlock;
using System;
using System.Collections.Generic;

namespace RPGConsole
{
    public class Generator
    {
        public List<Block> blockMap = new List<Block>();
        public int sizeMapX = 1200;
        public int sizeMapY = 1200;

        public Generator(int sizeX, int sizeY)
        {
            this.sizeMapX = sizeX;
            this.sizeMapY = sizeY;
            StartGenerate(false);
        }

        public Generator(List<Block> genBlocks)
        {
            //input blocks
            int maxX = 0;
            int maxY = 0;
            for(int i = 0; i < genBlocks.Count - 1; i++)
            {
                if(genBlocks[i].position.x > maxX)
                {
                    maxX = (int)genBlocks[i].position.x;
                }

                if(genBlocks[i].position.y > maxY)
                {
                    maxY = (int)genBlocks[i].position.y;
                }
            }

            //just to make it even from 127 to 128
            maxX++;
            maxY++;
            if (Program.debugMode) { Program.unit.AddConsoleItem("Size of generation is from imported file: " + maxX + ": " + maxY); }
            blockMap = genBlocks;
        }

        public void manualUpdate()
        {
            foreach(Block m in blockMap)
            {
                m.UpdateBlockThroughMiningandPlacing();
            }
        }

        public Block returnBlock(float x, float y)
        {
            return blockMap.Find(t => t.position.x == x && t.position.y == y);
        }

        public Block getBlock(string name)
        {
            foreach(Block blockm in blockMap)
            {
                if(blockm.name == name)
                {
                    return blockm;
                }
            }
            return null;
        }

        public void setBlock(int x, int y, Block block)
        {
            for (int i = 0; i < blockMap.Count; i++)
            {
                if (blockMap[i].position.x == x && blockMap[i].position.y == y)
                {
                    blockMap[i] = block;
                    blockMap[i].position = new Vector2(x, y);
                    blockMap[i].broken = false;
                }
            }
        }

        public void StartGenerate(bool randomize)
        {
            if (randomize == false)
            {
                FastNoiseLite noise = new FastNoiseLite();
                noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);

                //Gather noise data
                float[] noiseData = new float[sizeMapX * sizeMapY];
                int index = 0;

                for (int x = 0; x < sizeMapX; x++)
                {
                    for (int y = 0; y < sizeMapY; y++)
                    {
                        noiseData[index++] = noise.GetNoise(x, y);
                        Block block = new Air();
                        float noiseValue = noiseData[index - 1];
                        block.perlinvalue = noiseValue;
                        //dirt or mud
                        if (noiseValue <= 0)
                        {
                            block = new Dirt();
                        }else if(noiseValue < 0.10f)
                        {
                            block = new Mud();
                        }
                        else if (noiseValue > 0.10)
                        {
                            Random range = new Random();
                            int randomRange = range.Next(10);
                            switch (randomRange)
                            {
                                default:
                                    block = new Grass();
                                    break;
                                case 10:
                                    block = new Sapling();
                                    break;
                            }
                        }
                        else
                        {
                            Random range = new Random();
                            int randomRange = range.Next(40);
                            switch (randomRange)
                            {
                                case 25:
                                case 26:
                                    block = new IronOre();
                                    break;
                                case 40:
                                    block = new DiamondOre();
                                    break;
                                default:
                                    block = new Rock();
                                    break;
                            }
                        }
                        Vector2 newPosition = new Vector2(x, y);
                        block.position = newPosition;
                        blockMap.Add(block);
                    }
                }

                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "finished generating map!"));
                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "creating image out of created map"));
            }
            else
            {
                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "selected randomize mode"));
                for (int x = 0; x < sizeMapX; x++)
                {
                    for (int y = 0; y < sizeMapY; y++)
                    {
                        Random random = new Random();
                        int rand = random.Next(9);
                        Block block = null;
                        switch (rand)
                        {
                            case 0:
                                block = new Dirt();
                                break;
                            case 1:
                                block = new Grass();
                                break;
                            case 2:
                                block = new Mud();
                                break;
                            case 3:
                                block = new Rock();
                                break;
                            case 4:
                                block = new Sapling();
                                break;
                            case 5:
                                block = new Lava();
                                break;
                            case 6:
                                block = new IronOre();
                                break;
                            case 7:
                                block = new DiamondOre();
                                break;
                            case 8:
                                block = new Water();
                                break;
                            default:
                                block = new Air();
                                break;
                        }
                        Vector2 position = new Vector2(x, y);
                        block.position = position;
                        blockMap.Add(block);
                    }
                }
            }
        }
    }
}
