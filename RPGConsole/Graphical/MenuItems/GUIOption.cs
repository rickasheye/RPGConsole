using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical.MenuItems
{
    public class GUIOption
    {
        public string name;
        public bool focused;
        public Vector2 size = new Vector2(0, 0);
        public Vector2 position = new Vector2(0, 0);
        public bool disabled = false;

        public GUIOption parent;
        public List<GUIOption> children = new List<GUIOption>();

        public GUIOption(Vector2 size, Vector2 position)
        {
            this.size = size;
            this.position = position;
        }

        public GUIOption(string name, Vector2 size, Vector2 position)
        {
            this.name = name;
            this.size = size;
            this.position = position;
        }

        public virtual void DrawObject()
        {
            if(disabled == true)
            {
                return;
            }
        }

        public virtual void Start()
        {

        }
    }
}
