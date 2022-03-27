using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RPGConsole.Graphical
{
    public class Scene
    {
        public string name;
        public int id;

        bool loadedScene = false;

        public Scene(int id)
        {
            this.id = id;
        }

        public Scene(string name)
        {
            this.name = name;
            this.id = Program.loader.currentScenes.Count + 1;
        }

        public virtual void LoadScene()
        {
        }

        bool keyboardOnly = true;
        bool debugGUI = false;
        public virtual void UpdateScene()
        {

            if(loadedScene == false)
            {
                LoadScene();
                loadedScene = true;
            }

            List<GUIOption> newOptions = new List<GUIOption>();
            newOptions.AddRange(guiOptions);
            GUIOption renderContainer = null;
            if(renderContainer != null)
            {
                renderContainer.DrawObject();
            }

            int i = 0;
            foreach (GUIOption option in newOptions)
            {
                if(option is EmptyContainer && i <= 0)
                {
                    newOptions.Insert(0, option);
                }
                option.DrawObject();
                if (!keyboardOnly)
                {
                    if (Raylib.GetMouseX() > option.position.x && Raylib.GetMouseY() > option.position.y && Raylib.GetMouseX() < option.position.x + option.size.x && Raylib.GetMouseY() < option.position.y + option.size.y)
                    {
                        option.focused = true;
                    }
                    else
                    {
                        option.focused = false;
                    } 
                }

                if (debugGUI)
                {
                    Raylib.DrawRectangleLines((int)option.position.x, (int)option.position.y, (int)option.size.x, (int)option.size.y, Color.GRAY);
                    if (option.focused)
                    {
                        Raylib.DrawRectangle((int)option.position.x, (int)option.position.y, (int)option.size.x, (int)option.size.y, Color.GREEN);
                    }
                    else
                    {
                        Raylib.DrawRectangle((int)option.position.x, (int)option.position.y, (int)option.size.x, (int)option.size.y, Color.RED);
                    }
                }
                i++;
            }
            guiOptions.Clear();
            guiOptions.AddRange(newOptions);
            newOptions.Clear();
            newOptions = null;
        }
    }
}
