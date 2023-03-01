using System.Collections.Generic;
using UnityEngine;

namespace CaptainCoder.CraftingSystem.UnityEngine
{
    public class ShapelessRecipeData<T> : ScriptableObject where T : IItem
    {
        [field: SerializeField]
        public List<T> Ingredients { get; private set; }
        [field: SerializeField]
        public CraftingCategoryData Category { get; private set; }
        [field: SerializeField]
        public List<T> Results { get; private set; }
        
        public ShapelessRecipe<T> AsRecipe => new (Ingredients, Category.AsStruct, Results);
    }
}