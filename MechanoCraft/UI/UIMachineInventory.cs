using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using GeonBit.UI.Entities.TextValidators;
using GeonBit.UI.DataTypes;
using GeonBit.UI.Utils;

namespace MechanoCraft.UI
{
    public class UIMachineInventory
    {
        public Panel panel;
        private Panel leftPanel;
        private Panel centerPanel;
        private Panel rightPanel;
        public void BasePanel(string itemName)
        {
            panel = new Panel(new Vector2(400, 220), PanelSkin.Default, Anchor.Center);
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
        }

        public void OneInputOutputUI()
        {

            leftPanel.AddChild(new Label("Input", Anchor.TopCenter));
            leftPanel.AddChild(new Panel(new Vector2(64, 64), PanelSkin.Fancy, Anchor.AutoCenter));
            centerPanel.AddChild(new ProgressBar(0, 1, new Vector2(128, 32), Anchor.TopCenter, new Vector2(0, 42)));
            rightPanel.AddChild(new Label("Output", Anchor.TopCenter));
            rightPanel.AddChild(new Panel(new Vector2(64, 64), PanelSkin.Fancy, Anchor.AutoCenter));

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
