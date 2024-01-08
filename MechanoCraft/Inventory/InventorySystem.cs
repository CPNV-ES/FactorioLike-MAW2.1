using System.Collections.Generic;
using System.Linq;
using MechanoCraft.Inventory.Items;
using MonoGame.Extended.Entities;

namespace MechanoCraft.Inventory
{
    public class InventorySystem
    {
        private static InventorySystem instance;
        private Dictionary<IItem, Entity> worldResources;
        private InventorySystem()
        {
            worldResources = new Dictionary<IItem, Entity>();
        }

        public void RegisterItem(IItem item, Entity owner)
        {
            worldResources.Add(item, owner);
        }

        public void TransferItem(IItem item, Entity newOwner)
        {
            worldResources[item] = newOwner;
        }

        public void RemoveItem(IItem item)
        {
            worldResources.Remove(item);
        }

        public List<IItem> GetItems(Entity owner) 
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
