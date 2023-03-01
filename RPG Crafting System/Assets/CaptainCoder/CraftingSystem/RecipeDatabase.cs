using System;
using System.Collections.Generic;

namespace CaptainCoder.CraftingSystem
{
    public class RecipeDatabase<T> where T : IItem
    {
        private Dictionary<RecipeEntry, ShapelessRecipe<T>> _database;

        public bool TryGetRecipe(IEnumerable<T> ingredients, CraftingCategory category, out ShapelessRecipe<T> recipe)
        {
            recipe = null;
            return false;
        }

        public class RecipeEntry
        {
            private readonly Dictionary<T, int> _itemCounts = new();
            private readonly CraftingCategory _category;
            private readonly int _hashCode;

            public RecipeEntry(IEnumerable<T> ingredients, CraftingCategory category)
            {
                _hashCode = 0;
                foreach(T item in ingredients)
                {
                    _hashCode += item.GetHashCode();
                    if(!_itemCounts.ContainsKey(item))
                    {
                        _itemCounts[item] = 1;
                    }
                    else
                    {
                        _itemCounts[item]++;
                    }
                }
                _category = category;
                _hashCode = HashCode.Combine(_hashCode, _category);
            }

            public override bool Equals(object obj)
            {
                return obj is RecipeEntry entry &&
                        _hashCode == entry._hashCode &&
                        // TODO: Why does == not work here: _category == entry._category &&
                        _category.Equals(entry._category) && 
                        // TODO: Deep compare on incoming dictionary
                        true; 
                       //EqualityComparer<Dictionary<T, int>>.Default.Equals(_itemCounts, entry._itemCounts);    
            }

            public override int GetHashCode() => _hashCode;
        }

    }


}