using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical.MenuItems.KeyboardOnlyItems
{
    public class KeyboardAdjustedButton : Option
    {
        public KeyboardAdjustedButton(string text, Vector2 size, Vector2 position) : base(text, size, position)
        { }

        public override void DrawObject()
        {
            base.DrawObject();
            if (focused == true)
            {
                Raylib.DrawText("> " + name, (int)position.x, (int)position.y, (int)size.x / (int)size.y, Color.BLACK);
            }
            else if (focused == false)
            {
                Raylib.DrawText(name, (int)position.x, (int)position.y, (int)size.x / (int)size.y, Color.BLACK);
            }
        }
    }
}
