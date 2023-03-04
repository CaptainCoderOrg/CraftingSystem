using System.Collections.Generic;
using UnityEngine;

public class ShapelessRecipeData<T> : ScriptableObject, IShapelessRecipe<T>.IAdapter
{
    [field: SerializeField]
    public List<T> Ingredients { get; private set; }
    [field: SerializeField]
    public CraftingCategoryData Category { get; private set; }
    [field: SerializeField]
    public List<T> Results { get; private set; }
    private IShapelessRecipe<T> _recipe;
    public IShapelessRecipe<T> ShapelessRecipe => _recipe ??= new ShapelessRecipe<T>(Ingredients, Category, Results);
}
