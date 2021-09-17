namespace RPGConsole.InventoryItems
{

    public class InventoryItemWeapon : ItemUseable
    {
        public int damageLevel = 2;

        public override void UpgradeWeapon(Player player)
        {
            base.UpgradeWeapon(player);
            damageLevel = damageLevel * 2;
        }

        public InventoryItemWeapon(int damage, string name, itemMaterial mat, string texturePath) : base(name, itemTYPE.WEAPON, mat, texturePath) { }
    }
}
