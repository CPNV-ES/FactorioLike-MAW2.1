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
            Ore
        }
        public enum MaterialType
        {
            Copper,
            Iron
        }

        int id;
        string name;
    }
}
