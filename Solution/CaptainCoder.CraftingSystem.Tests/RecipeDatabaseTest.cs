#pragma warning disable NUnit2005 // Consider using Assert.That(actual, Is.EqualTo(expected)) instead of Assert.AreEqual(expected, actual)

public class RecipeDatabaseTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestRecipeEntryEquality()
    {
        string wood = "Wood";
        string rope = "Rope";
        List<string> ingredients1 = new() { rope, wood, wood, wood };
        List<string> ingredients2 = new() { wood, rope, wood, wood };
        List<string> ingredients3 = new() { wood, wood, rope, wood };
        List<string> ingredients0 = new() { wood, wood, wood, rope };
        CraftingCategory category = new("Wood Work");
        var entry0 = new RecipeDatabase<string>.RecipeEntry(ingredients0, category);
        var entry1 = new RecipeDatabase<string>.RecipeEntry(ingredients1, category);
        var entry2 = new RecipeDatabase<string>.RecipeEntry(ingredients2, category);
        var entry3 = new RecipeDatabase<string>.RecipeEntry(ingredients3, category);

        Assert.AreEqual(entry0, entry1);
        Assert.AreEqual(entry0, entry2);
        Assert.AreEqual(entry0, entry3);
        Assert.AreEqual(entry1, entry2);
        Assert.AreEqual(entry1, entry3);
        Assert.AreEqual(entry2, entry3);
    }

    [Test]
    public void TestTryGetRecipe()
    {
        string wood = "Wood";
        string rope = "Rope";
        string boat = "Boat";
        List<string> boatIngredients = new() { wood, wood, wood, rope };
        CraftingCategory woodWorkCategory = new("Wood Work");
        ShapelessRecipe<string> boatRecipe = new(boatIngredients, woodWorkCategory, new List<string>() { boat });

        string thread = new("Thread");
        List<string> ropeIngredients = new() { thread, thread, thread };
        CraftingCategory sewingCategory = new("Sewing");
        ShapelessRecipe<string> ropeRecipe = new(ropeIngredients, sewingCategory, new List<string>() { rope });

        ShapelessRecipe<string>[] recipes = new[] { boatRecipe, ropeRecipe };
        RecipeDatabase<string> database = new(recipes);

        Assert.True(database.TryGetRecipe(boatIngredients, woodWorkCategory, out IShapelessRecipe<string>? actual));
        Assert.AreEqual(boatRecipe, actual);

        Assert.False(database.TryGetRecipe(boatIngredients, sewingCategory, out _));
        Assert.True(database.TryGetRecipe(ropeIngredients, sewingCategory, out actual));
        Assert.AreEqual(ropeRecipe, actual);
    }

}
