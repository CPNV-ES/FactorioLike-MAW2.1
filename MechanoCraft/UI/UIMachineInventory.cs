using System.Collections.Generic;
using System.Numerics;
using GeonBit.UI;
using GeonBit.UI.Entities;
using GeonBit.UI.Utils;
using Microsoft.Xna.Framework.Graphics;
using MechanoCraft.Inventory.Items;

namespace MechanoCraft.UI
{
    public class UIMachineInventory
    {
        public Panel panel;
        public Button button;
        public static ProgressBar progressBar;
        public List<Item> inputItems;
        public List<Item> outputItems;
        private Panel leftPanel;
        private Panel centerPanel;
        private Panel rightPanel;
        private Panel inputPanel;
        private Panel outputPanel;

        public void BasePanel(string itemName)
        {
            panel = new Panel(new Vector2(400, 260), PanelSkin.Default, Anchor.Center);
            Panel entitiesGroup = new Panel(new Vector2(0, 250), PanelSkin.None, Anchor.Auto);

            UserInterface.Active.AddEntity(panel);

            panel.AddChild(new Header(itemName));
            panel.AddChild(new HorizontalLine());
            entitiesGroup.Padding = Vector2.Zero;
            panel.AddChild(entitiesGroup);


            var columnPanels = PanelsGrid.GenerateColums(3, entitiesGroup);
            foreach (var column in columnPanels) { column.Padding = Vector2.Zero; }
            leftPanel = columnPanels[0];
            centerPanel = columnPanels[1];
            rightPanel = columnPanels[2];
            button = new Button("Craft", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(200, 64), new Vector2(0, -74));
            centerPanel.AddChild(button);
        }

        public void OneInputOutputUI()
        {

            leftPanel.AddChild(new Label("Input", Anchor.TopCenter));
            inputPanel = new Panel(new Vector2(64, 64), PanelSkin.Fancy, Anchor.AutoCenter);
            //inputPanel.AddChild(new Image(texture, new Vector2(32, 32), drawMode: ImageDrawMode.Stretch, anchor: Anchor.Center));
            leftPanel.AddChild(inputPanel);

            progressBar = new ProgressBar(0, 100, new Vector2(128, 32), Anchor.TopCenter, new Vector2(0, 42));
            progressBar.Value = 0;
            centerPanel.AddChild(progressBar);

            
            rightPanel.AddChild(new Label("Output", Anchor.TopCenter));
            outputPanel = new Panel(new Vector2(64, 64), PanelSkin.Fancy, Anchor.AutoCenter);
            rightPanel.AddChild(outputPanel);
        }

        public void ChangeOutput(Texture2D texture)
        {
            outputPanel.AddChild(new Image(texture, new Vector2(32, 32), anchor: Anchor.TopCenter, offset: new Vector2(0, -16)));
        }

        public void ChangeInput(Texture2D texture)
        {
            inputPanel.AddChild(new Image(texture, new Vector2(32, 32), anchor: Anchor.TopCenter, offset: new Vector2(0, -16)));
        }

        public void DeletePanel()
        {
            if (panel != null && UserInterface.Active.Root.Find(panel.Identifier) != null)
            {
                UserInterface.Active.RemoveEntity(panel);
            }
        }
    }
}
