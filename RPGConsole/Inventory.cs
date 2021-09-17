using RPGConsole.InventoryItems;
using System;
using System.Collections.Generic;

namespace RPGConsole
{
    public class Inventory
    {
        public List<InventoryItem> items = new List<InventoryItem>();

        public bool itemExists(InventoryItem item)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].name == item.name)
                {
                    return true;
                }
            }
            return false;
        }

        public InventoryItem itemFind(InventoryItem item)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].name == item.name)
                {
                    return items[i];
                }
            }
            return null;
        }

        public void addItem(InventoryItem item)
        {
            if (itemExists(item))
            {
                itemFind(item).itemCount++;
                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "Item " + item.name + " has been increased in count in your inventory!"));
            }
            else
            {
                items.Add(item);
                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "Item " + item.name + " has been added to your inventory!"));
            }
        }

        public void removeItem(InventoryItem item)
        {
            if (itemExists(item))
            {
                if (itemFind(item).itemCount > 0)
                {
                    itemFind(item).itemCount--;
                    Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "one of " + item.name + " has been deducted from your inventory!"));
                }
                else
                {
                    for(int i = 0; i < items.Count; i++)
                    {
                        if(items[i].name == item.name)
                        {
                            items.RemoveAt(i);
                        }
                    }

                    Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "Item " + item.name + " has been removed from your inventory!"));
                }
            }
            else
            {
                Program.unit.AddConsoleItem(new Graphical.ConsoleItem(3, "Item doesnt exist!"));
            }
        }
    }
}
