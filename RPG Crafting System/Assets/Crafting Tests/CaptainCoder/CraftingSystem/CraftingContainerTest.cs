using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools; 
using System.Linq;
using CaptainCoder.Core;

namespace CaptainCoder.CraftingSystem
{

    public class CraftingContainerTest
    {
        private CraftingContainer<Item> cc2x3WoodWork;
        private Item wood;
        [SetUp]
        public void Setup()
        {
            cc2x3WoodWork = new (2, 3, new CraftingCategory("Wood Work"));
            wood = new Item("Wood");
        }

        // A Test behaves as an ordinary method
        [Test]
        public void TestConstructor2x3()
        {
            Assert.AreEqual(2, cc2x3WoodWork.Rows);
            Assert.AreEqual(3, cc2x3WoodWork.Columns);
            HashSet<CraftingCategory> expectedCategories = new () { new CraftingCategory("Wood Work") };
            Assert.AreEqual(0, cc2x3WoodWork.InvalidPositions.Count);
            Assert.AreEqual(0, cc2x3WoodWork.Positions.Count());
            Assert.True(expectedCategories.SetEquals(cc2x3WoodWork.Categories));
        }

        [Test]
        public void TestAddOneItem()
        {
            
            // Add Item
            // * Add to empty space
            // * Add to out of bounds space (invalid)
            // * Add to space with already existing item

            Assert.False(cc2x3WoodWork.HasItemAt(new Position(0, 0)));

            Assert.True(cc2x3WoodWork.TryAddItem(new Position(0, 0), wood));
            Assert.True(cc2x3WoodWork.HasItemAt(new Position(0, 0)));
            (Position pos, Item item)[] positions = cc2x3WoodWork.Positions.ToArray();
            Assert.AreEqual(1, positions.Length);
            Assert.AreEqual((new Position(0, 0), wood), positions[0]);
        }
    }
}