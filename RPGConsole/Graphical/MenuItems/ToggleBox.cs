using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RPGConsole.Graphical.MenuItems
{
    public class ToggleBox : GUIOption
    {
        public bool toggled = false;
        public string text;

        public ToggleBox(string text, Vector2 position, Vector2 size):base(size, position) { this.text = text; }

        public override void DrawObject()
        {
            base.DrawObject();
            Raylib.DrawRectangle((int)position.x, (int)position.y, (int)size.x, (int)size.y, Raylib_cs.Color.WHITE);
            Raylib.DrawRectangleLines((int)position.x, (int)position.y, (int)size.x, (int)size.y, Raylib_cs.Color.BLACK);
            if (toggled == false) { Raylib.DrawRectangle((int)position.x + 2, (int)position.y + 2, (int)size.x - 4, (int)size.y - 4, Color.RED); } else
            {
                Raylib.DrawRectangle((int)position.x + 2, (int)position.y + 2, (int)size.x - 4, (int)size.y - 4, Color.GREEN);
            }
            if (focused)
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                {
                    if(toggled == true)
                    {
                        toggled = false;
                    }else if(toggled == false)
                    {
                        toggled = true;
                    }
                }
            }
            Raylib.DrawText(text, (int)position.x + (int)size.x + 5, (int)position.y, (int)position.x - (int)position.y, Raylib_cs.Color.BLACK);
        }
    }
}
