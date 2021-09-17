using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical.MenuItems
{
    public class Button : GUIOption
    {
        public string text;
        public int textSize;
        public Color backColor;
        public Color backColorPressed;
        public Color foregroundColor;

        public Button(string text, Vector2 position, Vector2 size, int textSize, Color backColor, Color backColorPressed, Color foregroundColor):base(size, position) {
            this.text = text;
            this.textSize = textSize;
            this.backColor = backColor;
            this.backColorPressed = backColorPressed;
            this.foregroundColor = foregroundColor;
        }

        public override void DrawObject()
        {
            base.DrawObject();
            int posX = (int)position.x;
            int posY = (int)position.y;
            int sizeX = (int)size.x;
            int sizeY = (int)size.y;
            if (focused)
            {
                Raylib.DrawRectangle(posX, posY, sizeX, sizeY, backColorPressed);
                if (!Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) || !Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                {
                    MouseOverObject();
                }
                else
                {
                    TriggerObject();
                }
            }
            else
            {
                Raylib.DrawRectangle(posX, posY, sizeX, sizeY, backColor);
            }
            Raylib.DrawText(text, posX, posY, sizeX - sizeY, foregroundColor);
            Raylib.DrawRectangleLines(posX, posY, sizeX, sizeY, foregroundColor);
        }

        public virtual void MouseOverObject()
        {

        }

        public virtual void TriggerObject()
        {

        }
    }
}
