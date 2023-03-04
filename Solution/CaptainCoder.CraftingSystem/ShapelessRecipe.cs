public record ShapelessRecipe<T>(IEnumerable<T> Ingredients, ICraftingCategory Category, IEnumerable<T> Result) : IShapelessRecipe<T>
{
    public IEnumerable<T> Ingredients { get; init; } = Ingredients.AsEnumerable(); 
    public IEnumerable<T> Result { get; init; } = Result.AsEnumerable();
}