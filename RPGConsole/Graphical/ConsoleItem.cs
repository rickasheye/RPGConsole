using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical
{
    public class ConsoleItem
    {
        public int time;
        public string text;
        public string id = "";

        public ConsoleItem(int time, string text, string id)
        {
            this.time = time;
            this.text = text;
            this.id = id;
        }

        public ConsoleItem(int time, string text)
        {
            this.time = time;
            this.text = text;
        }
    }
}
