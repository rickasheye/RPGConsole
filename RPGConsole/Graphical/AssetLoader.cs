using Raylib_cs;
using RPGConsole.Graphical.MenuItems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RPGConsole.Graphical
{
    public class storedTexture
    {
        public string name;
        public string path;
        public Texture2D texture;

        public storedTexture(string name, string path, Texture2D text)
        {
            this.name = name;
            this.path = path;
            this.texture = text;
        }
    }


    public class AssetLoader
    {
        public List<storedTexture> texturesCached = new List<storedTexture>();

        public void UnloadAll()
        {
            foreach (storedTexture texture in texturesCached)
            {
                if (texture.name != string.Empty)
                {
                    Raylib.UnloadTexture(texture.texture);
                }
            }
            texturesCached.Clear();
        }

        //used to load all of the assets into game but since we have no assets yet nothing to load.
        public Texture2D textureLoad(string path)
        {
            try
            {
                if (!TextureExist(path))
                {
                    Image image = Raylib.LoadImage(path);
                    Raylib.ImageResizeNN(ref image, 64, 64);
                    Texture2D texture = Raylib.LoadTextureFromImage(image);
                    Raylib.UnloadImage(image);
                    Raylib.SetTextureFilter(texture, TextureFilterMode.FILTER_POINT);
                    TextureCreate(Path.GetFileName(path), path, texture);
                    return textureLoad(path);
                }
                else
                {
                    return getTexture(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e);
                return new Texture2D();
            }
        }

        public storedTexture TextureCreate(string name, string path, Texture2D texture)
        {
            if (!TextureExist(path))
            {
                storedTexture newTexure = new storedTexture(name, path, texture);
                texturesCached.Add(newTexure);
                Program.unit.AddConsoleItem("Created a new texture by the name of: " + name, 3);
                return newTexure;
            }
            else
            {
                Program.unit.AddConsoleItem("Texture already exists by the name of: " + name + "so unable to create a new storedTexture!", 3);
                return null;
            }
        }

        public Texture2D getTexture(string path)
        {
            foreach(storedTexture texturem in texturesCached)
            {
                if(texturem.path == path)
                {
                    return texturem.texture;
                }
            }
            return new Texture2D();
        }

        public void TextureRemove(string path)
        {
            if (TextureExist(path))
            {
                for(int i = 0; i < texturesCached.Count; i++)
                {
                    if(path == texturesCached[i].path)
                    {
                        texturesCached.RemoveAt(i);
                    }
                }
            }
            else
            {
                Program.unit.AddConsoleItem("Texture doesnt exist to be removed by this protocol?", 3);
            }
        }

        public bool TextureExist(string path)
        {
            foreach(storedTexture text in texturesCached)
            {
                if(text.path == path)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
