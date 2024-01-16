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
            possibleItems = new List<Item>();
            possibleItems.Add(new Item(0, "IronOre", Item.ItemType.Ore, Item.MaterialType.Iron, Item.StateType.Solid));
            possibleItems.Add(new Item(1, "Crafter", Item.ItemType.Crafter, Item.MaterialType.None, Item.StateType.Solid));
            possibleItems.Add(new Item(2, "Miner", Item.ItemType.Miner, Item.MaterialType.None, Item.StateType.Solid));
            possibleItems.Add(new Item(3, "Polisher", Item.ItemType.Polisher, Item.MaterialType.None, Item.StateType.Solid));
            possibleItems.Add(new Item(4, "Smelter", Item.ItemType.Smelter, Item.MaterialType.None, Item.StateType.Solid));
        }
    }
}
