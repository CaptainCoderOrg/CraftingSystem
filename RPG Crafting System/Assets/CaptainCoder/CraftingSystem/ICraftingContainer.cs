using System.Collections;
using System.Collections.Generic;
using CaptainCoder.Core;
using System.Diagnostics;
using System;
using System.Linq;
namespace CaptainCoder.CraftingSystem
{
    public interface ICraftingContainer<T> where T : IItem
    {
        public string Name { get; }
        public int Rows { get; }
        public int Columns { get; }
        public HashSet<ICraftingCategory> Categories { get; }
        public HashSet<Position> InvalidPositions { get; }
        public IEnumerable<(Position, T)> Positions { get; }

        /// <summary>
        /// Attempts to add the specified <paramref name="item"/> into this <see cref="CraftingContainer"/>
        /// at the specified <paramref name="position"/>. 
        /// Returns true, if the item was added and false otherwise.
        /// </summary>
        public bool TryAddItem(Position position, T item);
        public bool TryMove(Position from, Position to);
        public bool TryRemove(Position position, out T removed);
        public bool TryItemAt(Position position, out T result);
        public T ItemAt(Position position);
        public bool HasItemAt(Position position);

        public interface IAdapter : ICraftingContainer<T>
        {
            public ICraftingContainer<T> CraftingContainerDelegate { get; }
            string ICraftingContainer<T>.Name => CraftingContainerDelegate.Name;
            int ICraftingContainer<T>.Rows => CraftingContainerDelegate.Rows;
            int ICraftingContainer<T>.Columns => CraftingContainerDelegate.Columns;
            HashSet<ICraftingCategory> ICraftingContainer<T>.Categories => CraftingContainerDelegate.Categories;
            HashSet<Position> ICraftingContainer<T>.InvalidPositions => CraftingContainerDelegate.InvalidPositions;
            IEnumerable<(Position, T)> ICraftingContainer<T>.Positions => CraftingContainerDelegate.Positions;
            bool ICraftingContainer<T>.TryAddItem(Position position, T item) => CraftingContainerDelegate.TryAddItem(position, item);
            bool ICraftingContainer<T>.TryMove(Position from, Position to) => CraftingContainerDelegate.TryMove(from, to);
            bool ICraftingContainer<T>.TryRemove(Position position, out T removed) => CraftingContainerDelegate.TryRemove(position, out removed);
            bool ICraftingContainer<T>.TryItemAt(Position position, out T result) => CraftingContainerDelegate.TryItemAt(position, out result);
            T ICraftingContainer<T>.ItemAt(Position position) => CraftingContainerDelegate.ItemAt(position);
            bool ICraftingContainer<T>.HasItemAt(Position position) => CraftingContainerDelegate.HasItemAt(position);

        }
    }


}