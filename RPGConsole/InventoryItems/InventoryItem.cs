using Raylib_cs;
using RPGConsole.EngineStuff;
using RPGConsole.Graphical;

namespace RPGConsole.InventoryItems
{
    public enum itemTYPE
    {
        WEAPON, TOOL, GENERAL
    }

    public enum entityType
    {
        BLOCK, ITEM
    }

    public class InventoryItem : GameObject
    {
        public string name;
        public itemTYPE type;
        public entityType typeMaterial = entityType.ITEM;
        public int itemCount = 1;

        public string texture = "Textures/blocks/air.png";

        public Texture2D textureInit(AssetLoader loader)
        {
            return loader.textureLoad(texture);
        }

        public InventoryItem(string name, itemTYPE type) : base(name)
        {
            this.name = name;
            this.type = type;
        }

        public InventoryItem(string name, itemTYPE type, string texturePath) : base(name)
        {
            this.name = name;
            this.type = type;
            this.texture = texturePath;
        }

        public InventoryItem(string name, itemTYPE type, int itemCount) : base(name)
        {
            this.name = name;
            this.type = type;
            this.itemCount = itemCount;
        }

        public InventoryItem(string name, itemTYPE type, int itemCount, string texturePath) : base(name)
        {
            this.name = name;
            this.type = type;
            this.itemCount = itemCount;
            this.texture = texturePath;
        }

        public InventoryItem(string name, itemTYPE type, int itemCount, entityType typeEntity) : base(name)
        {
            this.name = name;
            this.type = type;
            this.itemCount = itemCount;
            this.typeMaterial = typeEntity;
        }

        public InventoryItem(string name, itemTYPE type, int itemCount, entityType typeEntity, string texturePath) :base(name)
        {
            this.name = name;
            this.type = type;
            this.itemCount = itemCount;
            this.typeMaterial = typeEntity;
            texture = texturePath;
        }

        public InventoryItem()
        {
            if (Program.debugMode == true) { Program.unit.AddConsoleItem("ITEM IS EMPTY and but called: " + name); }
        }
    }
}
