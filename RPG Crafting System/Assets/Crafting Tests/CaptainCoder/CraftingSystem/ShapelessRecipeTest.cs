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
            Item wood = new ("Wood");
            Item rope = new ("Rope");
            Item boat = new ("Boat");
            List<Item> boatIngredients = new () { wood, wood, wood, rope };
            CraftingCategory woodWorkCategory = new ("Wood Work");
            ShapelessRecipe<Item> boatRecipe = new (boatIngredients, woodWorkCategory, new List<Item>(){ boat } );

            Assert.True(boatRecipe.Result.Contains(boat));
            Assert.AreEqual(woodWorkCategory, boatRecipe.Category);

            void TestIngredients()
            {
                Assert.AreEqual(4, boatRecipe.Ingredients.Count());
                List<Item> expected = new () { wood, rope, wood, wood };
                foreach (Item ingredient in boatRecipe.Ingredients)
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
            Item wood = new ("Wood");
            Item boat = new ("Boat");
            List<Item> boatSalvageIngredients = new () { boat };
            CraftingCategory salvageCategory = new ("Salvage");
            ShapelessRecipe<Item> boatSalvageRecipe = new (boatSalvageIngredients, salvageCategory, new List<Item>(){ wood, wood, wood } );

            Assert.AreEqual(1, boatSalvageRecipe.Ingredients.Count());
            Assert.True(boatSalvageRecipe.Ingredients.Contains(boat));
            Assert.AreEqual(salvageCategory, boatSalvageRecipe.Category);

            void TestResults()
            {
                Assert.AreEqual(3, boatSalvageRecipe.Result.Count());
                List<Item> expected = new () { wood, wood, wood };
                foreach (Item ingredient in boatSalvageRecipe.Result)
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