using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CaptainCoder.CraftingSystem
{
    public interface IShapelessRecipe<T> where T : IItem
    {
        public abstract IEnumerable<T> Ingredients { get; }
        public IEnumerable<T> Result { get; }
        public ICraftingCategory Category { get; }

        public interface IAdapter : IShapelessRecipe<T>
        {
            public IShapelessRecipe<T> RecipeDelegate { get; }
            IEnumerable<T> IShapelessRecipe<T>.Ingredients => RecipeDelegate.Ingredients;
            IEnumerable<T> IShapelessRecipe<T>.Result => RecipeDelegate.Result;
            ICraftingCategory IShapelessRecipe<T>.Category => RecipeDelegate.Category;
        }
    }


}