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
       
        public int Rows { get; }
        public int Columns { get; }
        public HashSet<ICraftingCategory> Categories {get; }
        public HashSet<Position> InvalidPositions  { get; }
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
    }
}