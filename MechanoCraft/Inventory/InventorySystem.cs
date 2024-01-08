using System.Collections.Generic;
using System.Linq;
using MechanoCraft.Inventory.Items;
using MonoGame.Extended.Entities;

namespace MechanoCraft.Inventory
{
    public class InventorySystem
    {
        private static InventorySystem instance;
        private Dictionary<Item, Entity> worldResources;
        private InventorySystem()
        {
            worldResources = new Dictionary<Item, Entity>();
        }

        public void RegisterItem(Item item, Entity owner)
        {
            worldResources.Add(item, owner);
        }

        public void TransferItem(Item item, Entity newOwner)
        {
            worldResources[item] = newOwner;
        }

        public void RemoveItem(Item item)
        {
            worldResources.Remove(item);
        }

        public List<Item> GetItems(Entity owner) 
        {
            return worldResources.Keys.Where(item => worldResources[item] == owner).ToList();
        }

        public static InventorySystem GetInstance()
        {
            if (instance == null)
            {
                instance = new InventorySystem();
            }
            return instance;
        }
    }
}
