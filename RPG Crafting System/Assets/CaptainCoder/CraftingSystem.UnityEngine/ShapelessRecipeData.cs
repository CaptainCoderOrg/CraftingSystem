using System.Collections.Generic;
using UnityEngine;

namespace CaptainCoder.CraftingSystem.UnityEngine
{
    public class ShapelessRecipeData<T> : ScriptableObject, IShapelessRecipe<T> where T : IItem
    {
        [field: SerializeField]
        public List<T> Ingredients { get; private set; }
        [field: SerializeField]
        public CraftingCategoryData Category { get; private set; }
        [field: SerializeField]
        public List<T> Results { get; private set; }
        
        public ShapelessRecipe<T> AsRecipe => new (Ingredients, Category, Results);

        IEnumerable<T> IShapelessRecipe<T>.Ingredients => Ingredients;
        IEnumerable<T> IShapelessRecipe<T>.Result => Results;
        ICraftingCategory IShapelessRecipe<T>.Category => Category;
    }
}