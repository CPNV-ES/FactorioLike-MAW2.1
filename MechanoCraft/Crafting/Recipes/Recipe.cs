using MechanoCraft.Inventory.Items;
using System.Collections.Generic;

namespace MechanoCraft.Crafting.Recipes
{
    public class Recipe
    {
        public List<Item> inputs = new List<Item>();
        public List<Item> outputs = new List<Item>();

        public Recipe(List<Item> inputs, List<Item> outputs)
        {
            this.inputs.AddRange(inputs);
            this.outputs.AddRange(outputs);
        }
    }
}
