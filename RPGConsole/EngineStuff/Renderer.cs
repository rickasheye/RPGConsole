using Raylib_cs;
using RPGConsole.Graphical;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RPGConsole.EngineStuff
{
    public class Renderer
    {
        public enum renderer
        {
            RAYLIB
        }

        //the main renderer we can use to help change and manipulate this game
        public renderer render = renderer.RAYLIB;

        public void RenderText(string text, int x, int pos, int size, System.Drawing.Color color)
        {
            switch (render)
            {
                case renderer.RAYLIB:
                    break;
                default:
                    Program.unit.AddConsoleItem(new ConsoleItem(3, "Unfortunately unable to load since this renderer is invalid!"));
                    break;
            }
        }
    }
}
