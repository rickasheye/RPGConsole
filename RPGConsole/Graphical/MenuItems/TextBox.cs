using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical.MenuItems
{
    public class TextBox : GUIOption
    {
        public string text;
        public int textSize;
        public Color textColor;
        public Color foregroundColor;

        public TextBox(string text, Vector2 position, Vector2 size, int textSize, Color textColor, Color foregroundColor):base(size, position)
        {
            this.text = text;
            this.textSize = textSize;
            this.textColor = textColor;
            this.foregroundColor = foregroundColor;
        }

        public override void DrawObject()
        {
            base.DrawObject();
            Raylib.DrawRectangleLines((int)position.x, (int)position.y, (int)size.x, (int)size.y, foregroundColor);
            if (focused)
            {
                Raylib.DrawLine((int)position.x * text.Length, (int)position.y, (int)position.x, (int)position.y + (int)size.x - (int)size.y, textColor);
                int key = Raylib.GetKeyPressed();
                switch (key)
                {
                    case 0:
                        break;
                    case 81:
                        text += "Q";
                        break;
                    case 87:
                        text += "W";
                        break;
                    case 69:
                        text += "E";
                        break;
                    case 82:
                        text += "R";
                        break;
                    case 84:
                        text += "T";
                        break;
                    case 89:
                        text += "Y";
                        break;
                    case 85:
                        text += "U";
                        break;
                    case 73:
                        text += "I";
                        break;
                    case 79:
                        text += "O";
                        break;
                    case 80:
                        text += "P";
                        break;
                    case 65:
                        text += "A";
                        break;
                    case 83:
                        text += "S";
                        break;
                    case 68:
                        text += "D";
                        break;
                    case 70:
                        text += "F";
                        break;
                    case 71:
                        text += "G";
                        break;
                    case 72:
                        text += "H";
                        break;
                    case 74:
                        text += "J";
                        break;
                    case 75:
                        text += "K";
                        break;
                    case 76:
                        text += "L";
                        break;
                    case 90:
                        text += "Z";
                        break;
                    case 88:
                        text += "X";
                        break;
                    case 67:
                        text += "C";
                        break;
                    case 86:
                        text += "V";
                        break;
                    case 66:
                        text += "B";
                        break;
                    case 78:
                        text += "N";
                        break;
                    case 77:
                        text += "M";
                        break;
                    case 32:
                        text += " ";
                        break;
                    case 259:
                        if (text.Length > 0)
                        {
                            text = text.Remove(text.Length - 1); 
                        }
                        break;
                }
            }
            Raylib.DrawText(text, (int)position.x, (int)position.y, (int)size.x - (int)size.y, textColor);
        }
    }
}
