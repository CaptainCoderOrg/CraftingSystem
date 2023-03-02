using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools; 
using System.Linq;

namespace CaptainCoder.CraftingSystem
{

    public class RecipeDatabaseTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestRecipeEntryEquality()
        {
            SimpleItem wood = new ("Wood");
            SimpleItem rope = new ("Rope");
            List<SimpleItem> ingredients1 = new () { rope, wood, wood, wood };
            List<SimpleItem> ingredients2 = new () { wood, rope, wood, wood };
            List<SimpleItem> ingredients3 = new () { wood, wood, rope, wood };
            List<SimpleItem> ingredients0 = new () { wood, wood, wood, rope };
            CraftingCategory category = new ("Wood Work");
            var entry0 = new RecipeDatabase<SimpleItem>.RecipeEntry(ingredients0, category);
            var entry1 = new RecipeDatabase<SimpleItem>.RecipeEntry(ingredients1, category);
            var entry2 = new RecipeDatabase<SimpleItem>.RecipeEntry(ingredients2, category);
            var entry3 = new RecipeDatabase<SimpleItem>.RecipeEntry(ingredients3, category);

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
            SimpleItem wood = new ("Wood");
            SimpleItem rope = new ("Rope");
            SimpleItem boat = new ("Boat");
            List<SimpleItem> boatIngredients = new () { wood, wood, wood, rope };
            CraftingCategory woodWorkCategory = new ("Wood Work");
            ShapelessRecipe<SimpleItem> boatRecipe = new (boatIngredients, woodWorkCategory, new List<SimpleItem>(){ boat } );

            SimpleItem thread = new ("Thread");
            List<SimpleItem> ropeIngredients = new () { thread, thread, thread };
            CraftingCategory sewingCategory = new ("Sewing");
            ShapelessRecipe<SimpleItem> ropeRecipe = new (ropeIngredients, sewingCategory, new List<SimpleItem>(){ rope } );

            ShapelessRecipe<SimpleItem>[] recipes = new []{ boatRecipe, ropeRecipe };
            RecipeDatabase<SimpleItem> database = new (recipes);

            Assert.True(database.TryGetRecipe(boatIngredients, woodWorkCategory, out IShapelessRecipe<SimpleItem> actual));
            Assert.AreEqual(boatRecipe, actual);

            Assert.False(database.TryGetRecipe(boatIngredients, sewingCategory, out _));
            Assert.True(database.TryGetRecipe(ropeIngredients, sewingCategory, out actual));
            Assert.AreEqual(ropeRecipe, actual);        
        }

    }
}