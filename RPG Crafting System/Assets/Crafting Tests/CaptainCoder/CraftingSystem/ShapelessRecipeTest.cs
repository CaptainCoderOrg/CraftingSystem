using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools; 
using System.Linq;

namespace CaptainCoder.CraftingSystem
{

    public class ShapelessRecipeTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestConstructorWithBoatRecipe()
        {
            SimpleItem wood = new ("Wood");
            SimpleItem rope = new ("Rope");
            SimpleItem boat = new ("Boat");
            List<SimpleItem> boatIngredients = new () { wood, wood, wood, rope };
            CraftingCategory woodWorkCategory = new ("Wood Work");
            ShapelessRecipe<SimpleItem> boatRecipe = new (boatIngredients, woodWorkCategory, new List<SimpleItem>(){ boat } );

            Assert.True(boatRecipe.Result.Contains(boat));
            Assert.AreEqual(woodWorkCategory, boatRecipe.Category);

            void TestIngredients()
            {
                Assert.AreEqual(4, boatRecipe.Ingredients.Count());
                List<SimpleItem> expected = new () { wood, rope, wood, wood };
                foreach (SimpleItem ingredient in boatRecipe.Ingredients)
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
            SimpleItem wood = new ("Wood");
            SimpleItem boat = new ("Boat");
            List<SimpleItem> boatSalvageIngredients = new () { boat };
            CraftingCategory salvageCategory = new ("Salvage");
            ShapelessRecipe<SimpleItem> boatSalvageRecipe = new (boatSalvageIngredients, salvageCategory, new List<SimpleItem>(){ wood, wood, wood } );

            Assert.AreEqual(1, boatSalvageRecipe.Ingredients.Count());
            Assert.True(boatSalvageRecipe.Ingredients.Contains(boat));
            Assert.AreEqual(salvageCategory, boatSalvageRecipe.Category);

            void TestResults()
            {
                Assert.AreEqual(3, boatSalvageRecipe.Result.Count());
                List<SimpleItem> expected = new () { wood, wood, wood };
                foreach (SimpleItem ingredient in boatSalvageRecipe.Result)
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
}