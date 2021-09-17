using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical.MenuItems
{
    public class Text : GUIOption
    {
        public string text;
        public int textSize;
        public Color textColor;

        public Text(string text, Vector2 position, int textSize, Color textColor):base(new Vector2(0, 0), position)
        {
            this.text = text;
            this.textSize = textSize;
            this.textColor = textColor;
        }

        public override void DrawObject()
        {
            base.DrawObject();
            Raylib.DrawText(text, (int)position.x, (int)position.y, textSize, textColor);
        }
    }
}
