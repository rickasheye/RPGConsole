namespace RPGConsole.InventoryBlock
{
    public class Air : Block
    {
        public Air() : base("Air", 0, 0, 1, Color.WHITESKINCOLOR, "Textures/blocks/air.png", 1) { 
            giveBlock = false; 
        }

        public override void MineBlock(Player player)
        {
        }
    }
}
