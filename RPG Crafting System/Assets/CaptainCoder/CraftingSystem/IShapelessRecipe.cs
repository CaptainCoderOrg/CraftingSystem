using System.Collections.Generic;

namespace CaptainCoder.CraftingSystem
{
    public interface IShapelessRecipe<T> where T : IItem
    {
        public IEnumerable<T> Ingredients { get; }
        public IEnumerable<T> Result { get; }
        public ICraftingCategory Category { get; }

        public interface IAdapter : IShapelessRecipe<T>
        {
            public IShapelessRecipe<T> ShapelessRecipe { get; }
            IEnumerable<T> IShapelessRecipe<T>.Ingredients => ShapelessRecipe.Ingredients;
            IEnumerable<T> IShapelessRecipe<T>.Result => ShapelessRecipe.Result;
            ICraftingCategory IShapelessRecipe<T>.Category => ShapelessRecipe.Category;
        }
    }
}