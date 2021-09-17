using RPGConsole.InventoryItems;
using System.Collections.Generic;

namespace RPGConsole.Crafting
{
    public class CraftingRecipe
    {
        public InventoryItem item1;
        public InventoryItem item2;
        public InventoryItem item3;
        public InventoryItem item4;
        public InventoryItem item5;
        public InventoryItem item6;
        public InventoryItem item7;
        public InventoryItem item8;
        public InventoryItem item9;
        public bool needTable = false;

        public InventoryItem output;

        public static List<InventoryItem> convertToList(CraftingRecipe recipe)
        {
            List<InventoryItem> itemConvert = new List<InventoryItem>();
            if (recipe.item1 != null)
            {
                itemConvert.Add(recipe.item1);
            }
            else if (recipe.item2 != null)
            {
                itemConvert.Add(recipe.item2);
            }
            else if (recipe.item3 != null)
            {
                itemConvert.Add(recipe.item3);
            }
            else if (recipe.item4 != null)
            {
                itemConvert.Add(recipe.item4);
            }
            else if (recipe.item5 != null)
            {
                itemConvert.Add(recipe.item5);
            }
            else if (recipe.item6 != null)
            {
                itemConvert.Add(recipe.item6);
            }
            else if (recipe.item7 != null)
            {
                itemConvert.Add(recipe.item7);
            }
            else if (recipe.item8 != null)
            {
                itemConvert.Add(recipe.item8);
            }
            else if (recipe.item9 != null)
            {
                itemConvert.Add(recipe.item9);
            }
            return itemConvert;
        }

        //Sport the compatibility to support more than one item but atleast 9 individual items
        public CraftingRecipe(InventoryItems.InventoryItem item, InventoryItems.InventoryItem item2, InventoryItem output, bool needTable)
        {
            //store
            this.item1 = item;
            this.item2 = item2;
            this.output = output;
            this.needTable = needTable;
        }

        public CraftingRecipe(InventoryItem item, InventoryItem item2, InventoryItem item3, InventoryItem output, bool needTable)
        {
            this.item1 = item;
            this.item2 = item2;
            this.item3 = item3;
            this.output = output;
            this.needTable = needTable;
        }

        public CraftingRecipe(InventoryItem item, InventoryItem item2, InventoryItem item3, InventoryItem item4, InventoryItem output, bool needTable)
        {
            this.item1 = item;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.output = output;
            this.needTable = needTable;
        }

        public CraftingRecipe(InventoryItem item, InventoryItem item2, InventoryItem item3, InventoryItem item4, InventoryItem item5, InventoryItem output, bool needTable)
        {
            this.item1 = item;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.output = output;
            this.needTable = needTable;
        }

        public CraftingRecipe(InventoryItem item, InventoryItem item2, InventoryItem item3, InventoryItem item4, InventoryItem item5, InventoryItem item6, InventoryItem output, bool needTable)
        {
            this.item1 = item;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item6;
            this.output = output;
            this.needTable = needTable;
        }

        public CraftingRecipe(InventoryItem item, InventoryItem item2, InventoryItem item3, InventoryItem item4, InventoryItem item5, InventoryItem item6, InventoryItem item7, InventoryItem output, bool needTable)
        {
            this.item1 = item;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item6;
            this.item7 = item7;
            this.output = output;
            this.needTable = needTable;
        }

        public CraftingRecipe(InventoryItem item, InventoryItem item2, InventoryItem item3, InventoryItem item4, InventoryItem item5, InventoryItem item6, InventoryItem item7, InventoryItem item8, InventoryItem output, bool needTable)
        {
            this.item1 = item;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item6;
            this.item7 = item7;
            this.item8 = item8;
            this.output = output;
            this.needTable = needTable;
        }

        public CraftingRecipe(InventoryItem item, InventoryItem item2, InventoryItem item3, InventoryItem item4, InventoryItem item5, InventoryItem item6, InventoryItem item7, InventoryItem item8, InventoryItem item9, InventoryItem output, bool needTable)
        {
            this.item1 = item;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item6;
            this.item7 = item7;
            this.item8 = item8;
            this.item9 = item9;
            this.output = output;
            this.needTable = needTable;
        }
    }
}
