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
        public static List<Recipe> possibleRecipes;

        public static void CreateRecipes()
        {
            List<Item> inputs = new List<Item>();
            List<Item> outputs = new List<Item>();


            possibleRecipes = new List<Recipe>();

            inputs.Add(ItemCreator.possibleItems[1]);
            outputs.Add(ItemCreator.possibleItems[2]);
            Recipe recipe = new Recipe(inputs, outputs);

            possibleRecipes.Add(recipe);

            inputs.Clear();
            outputs.Clear();

            inputs.Add(ItemCreator.possibleItems[2]);
            outputs.Add(ItemCreator.possibleItems[3]);
            recipe = new Recipe(inputs, outputs);

            possibleRecipes.Add(recipe);

            inputs.Clear();
            outputs.Clear();


        }

    }
}
