using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CaptainCoder.CraftingSystem
{
    public interface IShapelessRecipe<T> where T : IItem
    {
        public IEnumerable<T> Ingredients { get; }
        public IEnumerable<T> Result { get; }
        public ICraftingCategory Category { get; }
    }
}