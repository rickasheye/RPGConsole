using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical.MenuItems.KeyboardOnlyItems
{
    public class KeyboardAdjustedButtonOptional : KeyboardAdjustedButton
    {
        public string originalName = "";
        public KeyboardAdjustedButtonOptional(string name, Vector2 size, Vector2 position):base(name, size, position) { }
        public bool justbenormal = false;

        public override void Start()
        {
            base.Start();
            if (justbenormal == false)
            {
                //Try to get all of the children to count themselves
                this.originalName = name;
                name = name + " Other Options +" + children.Count; 
            }
        }

        public int index = 0;
        KeyboardAdjustedButton buttonAdjustSelected;

        public override void DrawObject()
        {
            base.DrawObject();
            if (justbenormal == false)
            {
                if (focused == true)
                {
                    //draw the others!
                    if (index > children.Count - 1)
                    {
                        index = 0;
                    }

                    if (index <= 0)
                    {
                        index = children.Count - 1;
                    }

                    if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                    {
                        index--;
                    }
                    else if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                    {
                        index++;
                    }

                    for (int i = 1; i < children.Count + 1; i++)
                    {
                        if (index == i)
                        {
                            KeyboardAdjustedButton buttonAdjust = children[i] as KeyboardAdjustedButton;
                            if (buttonAdjust != null)
                            {
                                name = buttonAdjust.name;
                                buttonAdjustSelected = buttonAdjust;
                            }
                        }
                    }
                }
                else
                {
                    index = 0;
                    name = originalName + " Other Options +" + children.Count;
                } 
            }
        }

        public override void Triggerable()
        {
            base.Triggerable();
            if (justbenormal == false)
            {
                if (focused)
                {
                    Program.unit.AddConsoleItem("Triggered!",3);
                    buttonAdjustSelected.Triggerable();
                } 
            }
        }
    }
}
