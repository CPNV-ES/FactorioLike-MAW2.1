using MechanoCraft.Crafting.Recipes;
using MechanoCraft.Inventory.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanoCraft.Entities.Machines
{
    public class Machine
    {
        public string Name {  get; set; }
        public bool IsPlaced{get; set;}
        public Recipe Recipe { get; set;}
    }
}
