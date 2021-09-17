using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical.MenuItems.KeyboardOnlyItems
{
    public class EmptyContainer : GUIOption
    {
        //just a empty object
        public int indexSelect;

        public EmptyContainer(Vector2 size, Vector2 position):base(size, position) {
        }

        bool warning = false;

        public override void Start()
        {
            base.Start();
            Console.WriteLine("There is " + children.Count + " elements");
            foreach(GUIOption option in children)
            {
                if(option is Text)
                {
                    children.Remove(option);
                }
            }
        }

        bool mouse = false;

        bool resize = false;

        public override void DrawObject()
        {
            base.DrawObject();
            //Raylib.DrawRectangle((int)position.x, (int)position.y, (int)size.x, (int)size.y, Color.WHITE);
            Raylib.DrawRectangleLines((int)position.x, (int)position.y, (int)size.x, (int)size.y, Raylib_cs.Color.BLACK);
            
            if (children.Count > 0)
            {
                if (mouse == false)
                {
                    //manipulate the different objects.
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
                    {
                        //use the gui to change things
                        indexSelect++;
                        if (indexSelect > children.Count - 1)
                        {
                            indexSelect = 0;
                        }
                        //Console.WriteLine("Increase in GUI index!" + indexSelect);
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
                    {
                        indexSelect--;
                        if (indexSelect < 0)
                        {
                            indexSelect = children.Count - 1;
                        }
                        //Console.WriteLine("Decrease in GUI index" + indexSelect);
                    }

                    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                    {
                        mouse = true;
                    }
                }
                else
                {
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP) || Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
                    {
                        mouse = false;
                    }

                    for(int i = 0; i <= children.Count - 1; i++)
                    {
                        GUIOption blankOption = children[i];
                        if (Raylib.GetMouseX() > blankOption.position.x && Raylib.GetMouseX() < blankOption.position.x + blankOption.size.x && Raylib.GetMouseY() > blankOption.position.y && Raylib.GetMouseY() < blankOption.position.y + blankOption.size.y)
                        {
                            //select this option then!
                            indexSelect = i;
                        }
                    }
                }
                for (int i = 0; i <= children.Count - 1; i++)
                {
                    children[i].focused = false;
                }
                children[indexSelect].focused = true;
            }
            else
            {
                Raylib.DrawText("No children found!", (int) position.x, (int) position.y, (int)size.x - (int)size.y, Color.BLACK);
                if (warning == false)
                {
                    Console.WriteLine("No children found!");
                    warning = true;
                }
            }

            //resize
            if(resize == false)
            {
                if(children.Count > 0)
                {
                    //children avaliable draw around
                    Vector2 start = children[0].position;

                    Vector2 childEndPosition = children[children.Count - 1].position;
                    Vector2 childEndSize = children[children.Count - 1].size;
                    Vector2 end = new Vector2(childEndPosition.x + childEndSize.x, childEndPosition.y + childEndSize.y);
                    position = new Vector2(start.x - 10, start.y - 10);
                    size = new Vector2(end.x + 10, end.y + 10);
                    resize = true;
                }
            }
        }
    }
}
