using Raylib_cs;

namespace RPGConsole.InventoryBlock
{
    public class MachinePart_Power : BlankMachinePart
    {
        public int maxPower = 5;

        public MachinePart_Power() : base("Power Machine") {
            powerLevel = maxPower;
        }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
            powerLevel = 0;
        }

        public override void UpdateBlockThroughMiningandPlacing()
        {
            base.UpdateBlockThroughMiningandPlacing();
            if(powerLevel < 5)
            {
                //disable the block?
                disabled = true;
            }
            else
            {
                powerLevel = maxPower;
                disabled = false;
            }
        }

        public override void UpdateBlock()
        {
            base.UpdateBlock();
            Raylib.DrawTexture(loaderAsset.textureLoad("Textures/overlays/lightningbolt.png"), (int)position.x, (int)position.y, Raylib_cs.Color.WHITE);
        }
    }
}
