using Raylib_cs;
using RPGConsole.GameEnemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical.MenuItems.KeyboardOnlyItems
{
    public class Option : GUIOption
    {

        //an option to use to select
        public Option(string titleName, Vector2 size, Vector2 position):base(titleName, size, position) {}

        public override void Start()
        {
            base.Start();
            if(parent == null)
            {
                Console.WriteLine("Option missing a parent to manipulate!");
            }
        }

        public override void DrawObject()
        {
            base.DrawObject();
            //use as a kinda update method!

            if(focused == true)
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    Triggerable();
                }
            }
        }

        public virtual void Triggerable()
        {

        }
    }
}
