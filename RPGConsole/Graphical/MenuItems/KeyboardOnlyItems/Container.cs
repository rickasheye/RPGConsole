using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical.MenuItems.KeyboardOnlyItems
{
    public class Container : GUIOption
    {
        //container class
        public enum Alignment
        {
            LEFT,RIGHT,UP,DOWN,MIDDLE
        }

        public Alignment alignment;

        public Container (Alignment alignment, Vector2 size, Vector2 position):base(size, position) {
            this.alignment = alignment;
        }

        public void ResetScaling()
        {
            //scale well?
            Vector2 Lastposition = new Vector2(0, 0);
            Vector2 Lastsize = new Vector2(0, 0);
            foreach(GUIOption option in children)
            {
                if (Lastposition != new Vector2(0, 0))
                {
                    Lastposition = option.position;
                    Lastsize = option.size;
                    switch (alignment)
                    {
                        case Alignment.DOWN:
                            option.position = new Vector2(position.x + (size.x / 2), position.y + size.y);
                            break;
                        case Alignment.LEFT:
                            option.position = new Vector2(position.x, position.y + (size.y / 2)) ;
                            break;
                        case Alignment.MIDDLE:
                            option.position = new Vector2(position.x + (size.x / 2), position.y + (size.y / 2));
                            break;
                        case Alignment.RIGHT:
                            option.position = new Vector2(position.x + size.x, position.y + (size.y / 2));
                            break;
                        case Alignment.UP:
                            option.position = new Vector2(position.x + (size.x / 2), position.y);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Unfortunately that element is at 0, 0 and not able to be sized!");
                }
            }
        }
    }
}
