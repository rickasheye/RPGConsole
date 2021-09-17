namespace RPGConsole.InventoryItems
{

    public class InventoryItemPickaxe : ItemUseable
    {
        public int damageLevel = 2;

        public override void UpgradeWeapon(Player player)
        {
            base.UpgradeWeapon(player);
            damageLevel = damageLevel * 2;
        }

        public InventoryItemPickaxe(int blockDamage, string name, itemMaterial mat, string texturePath) : base(name, itemTYPE.TOOL, mat, texturePath)
        {
            this.damageLevel = blockDamage;
        }
    }
}
