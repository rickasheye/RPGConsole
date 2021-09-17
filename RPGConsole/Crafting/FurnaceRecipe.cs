using RPGConsole.InventoryItems;

namespace RPGConsole.Crafting
{
    public class FurnaceRecipe
    {
        public InventoryItem input;
        public InventoryItem output;

        public FurnaceRecipe(InventoryItem input, InventoryItem output)
        {
            this.input = input;
            this.output = output;
        }
    }
}
