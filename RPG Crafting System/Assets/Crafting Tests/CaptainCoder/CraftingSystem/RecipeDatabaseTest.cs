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
            List<SimpleItem> ingredients0 = new () { wood, wood, wood, rope };
            List<SimpleItem> ingredients1 = new () { rope, wood, wood, wood };
            List<SimpleItem> ingredients2 = new () { wood, rope, wood, wood };
            List<SimpleItem> ingredients3 = new () { wood, wood, rope, wood };
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

    }
}