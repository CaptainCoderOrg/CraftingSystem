#pragma warning disable NUnit2005 // Consider using Assert.That(actual, Is.EqualTo(expected)) instead of Assert.AreEqual(expected, actual)

public class ShapelessRecipeTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestConstructorWithBoatRecipe()
    {
        string wood = "Wood";
        string rope = "Rope";
        string boat = "Boat";
        List<string> boatIngredients = new() { wood, wood, wood, rope };
        CraftingCategory woodWorkCategory = new("Wood Work");
        ShapelessRecipe<string> boatRecipe = new(boatIngredients, woodWorkCategory, new List<string>() { boat });

        Assert.True(boatRecipe.Result.Contains(boat));
        Assert.AreEqual(woodWorkCategory, boatRecipe.Category);

        void TestIngredients()
        {
            Assert.AreEqual(4, boatRecipe.Ingredients.Count());
            List<string> expected = new() { wood, rope, wood, wood };
            foreach (string ingredient in boatRecipe.Ingredients)
            {
                Assert.True(expected.Contains(ingredient));
                expected.Remove(ingredient);
            }
            Assert.AreEqual(0, expected.Count);
        }
        TestIngredients();
        TestIngredients();
    }

    [Test]
    public void TestConstructorWithBoatDeconstructRecipe()
    {
        string wood = "Wood";
        string boat = "Boat";
        List<string> boatSalvageIngredients = new() { boat };
        CraftingCategory salvageCategory = new("Salvage");
        ShapelessRecipe<string> boatSalvageRecipe = new(boatSalvageIngredients, salvageCategory, new List<string>() { wood, wood, wood });

        Assert.AreEqual(1, boatSalvageRecipe.Ingredients.Count());
        Assert.True(boatSalvageRecipe.Ingredients.Contains(boat));
        Assert.AreEqual(salvageCategory, boatSalvageRecipe.Category);

        void TestResults()
        {
            Assert.AreEqual(3, boatSalvageRecipe.Result.Count());
            List<string> expected = new() { wood, wood, wood };
            foreach (string ingredient in boatSalvageRecipe.Result)
            {
                Assert.True(expected.Contains(ingredient));
                expected.Remove(ingredient);
            }
            Assert.AreEqual(0, expected.Count);
        }
        TestResults();
        TestResults();
    }
}