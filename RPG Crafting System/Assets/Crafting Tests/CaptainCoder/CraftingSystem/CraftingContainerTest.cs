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
        private CraftingContainer<SimpleItem> _cc2x3WoodWork;
        private CraftingContainer<SimpleItem> _cc4x4WithNoCorners;
        private SimpleItem _wood;
        private SimpleItem _boat;
        private SimpleItem _rope;
        private SimpleItem _metalStud;
        
        [SetUp]
        public void Setup()
        {
            /*
              . . . 
              . . .
            */
            _cc2x3WoodWork = new ("Workbench", 2, 3, new CraftingCategory("Wood Work"));

            /*
               . .
             . . . .
             . . . .
               . .
            */
            _cc4x4WithNoCorners = new (
                "Oven",
                4, 4, 
                new ICraftingCategory[]{new CraftingCategory("Simple Food"), new CraftingCategory("Advanced Food")},
                new Position[]{(0,0), (0,3), (3,0), (3,3) }
                );
            _wood = new SimpleItem("Wood");
            _boat = new SimpleItem("Boat");
            _rope = new SimpleItem("Rope");
            _metalStud = new SimpleItem("Metal Stud");
        }

        // A Test behaves as an ordinary method
        [Test]
        public void TestConstructor2x3()
        {
            Assert.AreEqual(2, _cc2x3WoodWork.Rows);
            Assert.AreEqual(3, _cc2x3WoodWork.Columns);
            HashSet<ICraftingCategory> expectedCategories = new () { new CraftingCategory("Wood Work") };
            Assert.AreEqual(0, _cc2x3WoodWork.InvalidPositions.Count);
            Assert.AreEqual(0, _cc2x3WoodWork.Positions.Count());
            Assert.True(expectedCategories.SetEquals(_cc2x3WoodWork.Categories));
        }

        [Test, Timeout(5000), Description("Tests adding a single item to a crafting container")]
        public void TestAddOneItem()
        {
            
            // Add Item
            // * Add to empty space
            // * Add to out of bounds space (invalid)
            // * Add to space with already existing item

            Assert.False(_cc2x3WoodWork.HasItemAt((0, 0)));

            Assert.True(_cc2x3WoodWork.TryAddItem((0, 0), _wood));
            Assert.True(_cc2x3WoodWork.HasItemAt((0, 0)));
            (Position pos, SimpleItem item)[] positions = _cc2x3WoodWork.Positions.ToArray();
            Assert.AreEqual(1, positions.Length);
            Assert.AreEqual((new Position(0, 0), _wood), positions[0]);
        }

        [Test, Timeout(5000), Description("Tests adding an item to an already occupied position in a crafting container")]
        public void TestAddItemAtOccupied()
        {
            Assert.True(_cc2x3WoodWork.TryAddItem((0, 0), _wood));
            Assert.False(_cc2x3WoodWork.TryAddItem((0, 0), _wood));
        }

        [Test, Timeout(5000), Description("Tests adding several items to a container")]
        public void TestAddMultipleItems()
        {
            
            Assert.True(_cc2x3WoodWork.TryAddItem((0, 0), _wood));
            Assert.True(_cc2x3WoodWork.TryAddItem((1, 0), _wood));
            Assert.True(_cc2x3WoodWork.TryAddItem((0, 1), _rope));
            Assert.True(_cc2x3WoodWork.TryAddItem((0, 2), _metalStud));

            Assert.AreEqual(4, _cc2x3WoodWork.Positions.Count());
            Assert.True(_cc2x3WoodWork.HasItemAt((0, 0)));
            Assert.True(_cc2x3WoodWork.HasItemAt((1, 0)));
            Assert.True(_cc2x3WoodWork.HasItemAt((0, 1)));
            Assert.True(_cc2x3WoodWork.HasItemAt((0, 2)));

            HashSet<(Position pos, SimpleItem item)> expectedPositions = new ()
            {
                ((0, 2), _metalStud),
                ((0, 1), _rope),
                ((0, 0), _wood),
                ((1, 0), _wood),
            };
            HashSet<(Position pos, SimpleItem item)> positions = _cc2x3WoodWork.Positions.ToHashSet();
            Assert.True(expectedPositions.SetEquals(positions));
        }

        [Test, Timeout(5000), Description("Tests attempting to add an item to a position that is out of bounds")]
        [TestCase(-2, -2)]
        [TestCase(4, 4)]
        [TestCase(-2, 0)]
        [TestCase(0, 4)]
        [TestCase(0, 0)]
        [TestCase(3, 0)]
        [TestCase(3, 3)]
        [TestCase(0, 3)]
        public void TestAddItemOutOfBounds(int row, int col)
        {
            Assert.False(_cc4x4WithNoCorners.TryAddItem((row, col), _boat));
        }


        [Test, Timeout(5000), Description("Tests attempting to move an item to a position that is empty")]
        public void TestMoveItemEmpty()
        {
            _cc2x3WoodWork.TryAddItem((0, 0), _boat);
            Assert.True(_cc2x3WoodWork.TryMove((0, 0), (1, 1)));
            Assert.False(_cc2x3WoodWork.HasItemAt((0,0)));
            Assert.True(_cc2x3WoodWork.HasItemAt((1,1)));
            Assert.AreEqual(_boat, _cc2x3WoodWork.ItemAt((1,1)));
            Assert.AreEqual(1, _cc2x3WoodWork.Positions.Count());
            Assert.AreEqual((new Position(1, 1), _boat), _cc2x3WoodWork.Positions.ToArray()[0]);
        }

        [Test, Timeout(5000), Description("Tests attempting to move an item to an invalid position")]
        [TestCase(0,0)]
        [TestCase(3,0)]
        [TestCase(3,3)]
        [TestCase(0,3)]
        [TestCase(-1,0)]
        [TestCase(-2,-2)]
        [TestCase(4,0)]
        [TestCase(0,4)]
        [TestCase(4,4)]
        public void TestMoveItemInvalid(int row, int col)
        {
            _cc4x4WithNoCorners.TryAddItem((1, 1), _boat);
            Assert.True(_cc4x4WithNoCorners.TryMove((1, 1), (row, col)));
        }

        [Test, Timeout(5000), Description("Tests swapping items using Move")]
        public void TestMoveSwapItems()
        {
            _cc2x3WoodWork.TryAddItem((0, 0), _boat);
            _cc2x3WoodWork.TryAddItem((1, 2), _wood);
            _cc2x3WoodWork.TryMove((0,0), (1,2));
            Assert.AreEqual(_boat, _cc2x3WoodWork.ItemAt((1,2)));
            Assert.AreEqual(_wood, _cc2x3WoodWork.ItemAt((0,0)));
            Assert.AreEqual(2, _cc2x3WoodWork.Positions.Count());
        }

        [Test, Timeout(5000), Description("Tests removing from Crafting Container")]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void TestTryRemove(int row, int col)
        {
            Assert.False(_cc2x3WoodWork.TryRemove(new(row,col), out SimpleItem _));
            Assert.True(_cc2x3WoodWork.TryAddItem(new(row,col), _boat));
            Assert.True(_cc2x3WoodWork.HasItemAt(new(row,col)));
            Assert.True(_cc2x3WoodWork.TryRemove(new(row,col), out SimpleItem removed));
            Assert.AreEqual(_boat, removed);
            Assert.False(_cc2x3WoodWork.HasItemAt(new(row,col)));    
        }

    }
}