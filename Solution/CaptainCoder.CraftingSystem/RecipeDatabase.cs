public class RecipeDatabase<T>
{
    private readonly Dictionary<RecipeEntry, IShapelessRecipe<T>> _database;
    public RecipeDatabase(IEnumerable<IShapelessRecipe<T>> recipes)
    {
        if (recipes == null) { throw new ArgumentNullException("Recipes must be non-null."); }
        _database = new Dictionary<RecipeEntry, IShapelessRecipe<T>>();
        foreach (IShapelessRecipe<T> recipe in recipes)
        {
            _database[new RecipeEntry(recipe.Ingredients, recipe.Category)] = recipe;
        }
    }

    /// <summary>
    /// Given <paramref name="ingredients"/> and a <paramref
    /// name="category"/>, attempts to find a <paramref name="recipe"/> in
    /// the database. If one is found, returns true and populated <paramref
    /// name="recipe"/>. Otherwise, returns false and the value of <paramref
    /// name="recipe"/> is undefined.
    /// </summary>
    public bool TryGetRecipe(IEnumerable<T> ingredients, ICraftingCategory category, out IShapelessRecipe<T>? recipe)
    {
        RecipeEntry key = new RecipeEntry(ingredients, category);
        return _database.TryGetValue(key, out recipe);
    }

    public bool TryGetRecipe(IEnumerable<T> ingredients, ICraftingContainer<T> container, out IShapelessRecipe<T>? recipe)
    {
        foreach (ICraftingCategory category in container.Categories)
        {
            if (TryGetRecipe(ingredients, category, out recipe)) { return true; }
        }
        recipe = null;
        return false;
    }

    public class RecipeEntry
    {
        private readonly Dictionary<T, int> _itemCounts = new();
        private readonly ICraftingCategory _category;
        private readonly int _hashCode;

        public RecipeEntry(IEnumerable<T> ingredients, ICraftingCategory category)
        {
            _hashCode = 0;
            foreach (T item in ingredients)
            {
                _hashCode += item!.GetHashCode();
                if (!_itemCounts.ContainsKey(item))
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
                    _category.Equals(entry._category) &&
                    _itemCounts.KeyValuePairEquals(entry._itemCounts);
        }

        public override int GetHashCode() => _hashCode;
    }

}


