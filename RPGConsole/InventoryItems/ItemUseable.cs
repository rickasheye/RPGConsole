using System;

namespace RPGConsole.InventoryItems
{
    public enum itemMaterial
    {
        WOOD, STONE, METAL, DIAMOND
    }

    public class ItemUseable : InventoryItem
    {
        public itemMaterial mat;
        public int level = 1;
        public int weaponxp = 0;

        public virtual void UpgradeWeapon(Player player)
        {
            if (weaponxp >= level * 2)
            {
                level++;
                player.LevelUp();
                Console.WriteLine("your " + name + " has recently been upgraded to level " + level + "!");
            }
            else
            {
                weaponxp++;
            }
        }

        public ItemUseable(string name, itemTYPE type, itemMaterial mat) : base(name, type)
        {
            this.mat = mat;
        }

        public ItemUseable(string name, itemTYPE type, itemMaterial mat, string texturePath):base(name, type, texturePath) { }
    }
}
