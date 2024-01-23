using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MechanoCraft.Inventory.Items;
namespace MechanoCraft.Inventory
{
    public  class ItemCreator
    {
        public static  List<Item> possibleItems;

        public static void CreateItems()
        {
            possibleItems = new List<Item>
            {
                new Item(0, "None", Item.ItemType.None, Item.MaterialType.None, Item.StateType.Solid),
                new Item(1, "IronOre", Item.ItemType.Ore, Item.MaterialType.Iron, Item.StateType.Solid),
                new Item(2, "IronBar", Item.ItemType.Bar, Item.MaterialType.Iron, Item.StateType.Solid),
                new Item(3, "IronPlate", Item.ItemType.Plate, Item.MaterialType.Iron, Item.StateType.Solid),
                new Item(4, "Crafter", Item.ItemType.Crafter, Item.MaterialType.None, Item.StateType.Solid),
                new Item(5, "Miner", Item.ItemType.Miner, Item.MaterialType.None, Item.StateType.Solid),
                new Item(6, "Polisher", Item.ItemType.Polisher, Item.MaterialType.None, Item.StateType.Solid),
                new Item(4, "Smelter", Item.ItemType.Smelter, Item.MaterialType.None, Item.StateType.Solid)
            };
        }
    }
}
