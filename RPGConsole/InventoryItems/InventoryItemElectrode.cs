using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.InventoryItems
{
    public class InventoryItemElectrode : InventoryItem
    {
        public InventoryItemElectrode(int count):base("Electrode", itemTYPE.GENERAL, count) { }
    }
}
