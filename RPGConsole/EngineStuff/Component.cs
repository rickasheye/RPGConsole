using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.EngineStuff
{
    public class Component
    {
        public string name;

        public Component(string name)
        {
            this.name = name;
        }

        public Component()
        {
            this.name = "Untitled";
        }
    }
}
