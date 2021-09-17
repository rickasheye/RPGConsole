using Raylib_cs;
using RPGConsole.Graphical.MenuItems.KeyboardOnlyItems;
using RPGConsole.InventoryItems;
using System.Collections;

namespace RPGConsole.InventoryBlock
{
    public class ElectrodeNode : Block
    {
        public ElectrodeNode() : base("ElectrodeNode", 0, 0, 8, Color.WHITESKINCOLOR, "Textures/blocks/powernode.png", 1) {
            timeRefresh = timeRefresh * (int)Raylib.GetFrameTime();
        }

        int time = 0;
        int timeRefresh = 10000;

        public int bolts = 0;

        public override void UpdateBlock()
        {
            base.UpdateBlock();
            time++;
            if(time > timeRefresh)
            {
                bolts++;
            }
        }

        public override void PlayerOnTop(Player player)
        {
            base.PlayerOnTop(player);
            EmptyContainer container = new EmptyContainer(new Vector2(50, 50), new Vector2(10, 10));
            Program.loader.currentScene.guiOptions.Add(new collectElectrodesButton(player, this, new Vector2(50, 50), new Vector2(10, 10)));
            container.children.AddRange(Program.loader.currentScene.guiOptions);
            Program.loader.currentScene.guiOptions.Add(container);
        }

        public override void PlayerOffBlock(Player player)
        {
            base.PlayerOffBlock(player);
            Program.loader.currentScene.guiOptions.Clear();
        }
    }

    public class collectElectrodesButton : KeyboardAdjustedButton
    {
        ElectrodeNode node;
        Player player;

        public collectElectrodesButton(Player player, ElectrodeNode node, Vector2 size, Vector2 position):base("Collect " + node.bolts + " Electrodes!", size, position) {
            this.node = node;
            this.player = player;
        }

        public override void Triggerable()
        {
            base.Triggerable();
            player.inv.addItem(new InventoryItemElectrode(node.bolts));
            node.bolts = 0;
        }
    }
}
