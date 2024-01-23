using MechanoCraft.Inventory;
using MechanoCraft.Inventory.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanoCraft.Crafting.Recipes
{
    public class Recipes
    {
        static Dictionary<List<Item>, List<Item>> possibleRecipes;

        public static void CreateRecipes()
        {
            List<Item> inputs = new List<Item>();
            List<Item> outputs = new List<Item>();


            possibleRecipes = new Dictionary<List<Item>, List<Item>> ();

            inputs.Add(ItemCreator.possibleItems[1]);
            outputs.Add(ItemCreator.possibleItems[2]);
            possibleRecipes.Add(inputs, outputs);

            inputs.Clear();
            outputs.Clear();

            inputs.Add(ItemCreator.possibleItems[2]);
            outputs.Add(ItemCreator.possibleItems[3]);
            possibleRecipes.Add(inputs, outputs);

            inputs.Clear();
            outputs.Clear();


        }

    }
}
