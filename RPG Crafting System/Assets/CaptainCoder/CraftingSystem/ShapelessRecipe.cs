using System.Collections.Generic;
using System.Linq;
using System;
namespace CaptainCoder.CraftingSystem
{
    public class ShapelessRecipe<T> : IShapelessRecipe<T> where T : IItem
    {
        private readonly List<T> _ingredients;
        private readonly List<T> _result;
        public ShapelessRecipe(IEnumerable<T> ingredients, ICraftingCategory category, IEnumerable<T> result)
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
        public IEnumerable<T> Ingredients 
        {
            get
            {
                foreach(T ingredient in _ingredients)
                {
                    yield return ingredient;
                }
            }
        }
        public IEnumerable<T> Result 
        {
            get
            {
                foreach(T result in _result)
                {
                    yield return result;
                }
            }
        }
        public ICraftingCategory Category { get; }
    }
}