using MechanoCraft.Crafting.Recipes;
using MechanoCraft.Inventory.Items;
using System;
using System.Collections.Generic;

namespace MechanoCraft.Crafting
{
    public class CraftingSystem
    {
        public static List<Item> Craft(Recipe recipe, List<Item> inputs)
        {
            if (recipe == null)
            {
                throw new EmptyRecipeException();
            }

            if (inputs == null || inputs.Count == 0)
            {
                throw new EmptyInputException();
            }

            int index = 0;
            bool isCraftable = true;
           
                foreach (Item item in inputs)
                {
                    if (item.item == recipe.inputs[index].item)
                    {
                        isCraftable = true;
                    }
                    else
                    {
                        isCraftable = false;
                    }
                }
                if (isCraftable)
                {
                    return recipe.outputs;
                }

            return null;
        }
    }
}

class EmptyRecipeException : Exception { }
class EmptyInputException : Exception { }