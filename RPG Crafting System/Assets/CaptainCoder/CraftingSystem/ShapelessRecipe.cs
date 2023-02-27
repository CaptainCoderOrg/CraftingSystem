using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CaptainCoder.CraftingSystem
{
    public class ShapelessRecipe
    {
        private readonly List<IItem> _ingredients;
        private readonly List<IItem> _result;
        public ShapelessRecipe(IEnumerable<IItem> ingredients, RecipeCategory category, IEnumerable<IItem> result)
        {
            if (ingredients.Count() < 1) { throw new ArgumentException("Must have at least 1 ingredient."); }
            if (result.Count() < 1) { throw new ArgumentException("Must have at least 1 result item."); }
            _ingredients = ingredients.ToList();
            Category = category;
            _result = result.ToList();
        }

        /// <summary>
        /// Returns an IEnumerable of the items necessary
        /// to perform this recipe. The order is undefined.
        /// </summary>
        public IEnumerable<IItem> Ingredients 
        {
            get
            {
                foreach(IItem ingredient in _ingredients)
                {
                    yield return ingredient;
                }
            }
        }
        public IEnumerable<IItem> Result 
        {
            get
            {
                foreach(IItem result in _result)
                {
                    yield return result;
                }
            }
        }
        public RecipeCategory Category { get; }
        

    }
}