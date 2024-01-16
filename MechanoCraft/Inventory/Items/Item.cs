using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanoCraft.Inventory.Items
{
    public class Item
    {
        public enum ItemType
        {
            None,
            Ore,
            Crafter,
            Miner,
            Polisher,
            Smelter
        }
        public enum MaterialType
        {
            None,
            Copper,
            Iron
        }
        public enum StateType
        {
            Solid,
            Gaz,
            Liquid,
            Plasma
        }

        public Item(int id, string name, ItemType itemType, MaterialType materialType, StateType stateType)
        {
            this.id = id;
            this.name = name;
            this.item = itemType;
            this.material = materialType;
            this.state = stateType;
        }

        public int id;
        public string name;

        public MaterialType material;
        public ItemType item;
        public StateType state;
    }
}
