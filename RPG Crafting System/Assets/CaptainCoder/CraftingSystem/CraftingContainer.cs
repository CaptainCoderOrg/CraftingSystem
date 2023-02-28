using System.Collections;
using System.Collections.Generic;
using CaptainCoder.Core;
using System.Diagnostics;
using System; 
using System.Linq;
namespace CaptainCoder.CraftingSystem
{
    public class CraftingContainer<T> where T : IItem
    {
        private readonly Dictionary<Position, T> _grid;
        private readonly HashSet<Position> _invalidPositions;
        private readonly HashSet<CraftingCategory> _categories;

        public CraftingContainer(int rows, int columns, CraftingCategory category, IEnumerable<Position> invalidPositions = null) 
            : this(rows, columns, new []{category}, invalidPositions) {} 

        public CraftingContainer(int rows, int columns, IEnumerable<CraftingCategory> categories, IEnumerable<Position> invalidPositions = null)
        {
            if (columns < 1 || rows < 1) { throw new ArgumentException($"Invalid CraftingContainer size columns: {columns}, rows: {rows}"); };
            if (!categories.Any(_ => true)) { throw new ArgumentException($"CraftingContainer requires at least one {nameof(CraftingCategory)}."); }
            Rows = rows;
            Columns = columns;
            _invalidPositions = invalidPositions == null ? new HashSet<Position>() : invalidPositions.ToHashSet();
            _categories = categories.ToHashSet();
            _grid = new Dictionary<Position, T>();
        }

        public int Rows { get; }
        public int Columns { get; }
        public HashSet<CraftingCategory> Categories => _categories.ToHashSet();
        public HashSet<Position> InvalidPositions => _invalidPositions.ToHashSet();
        public IEnumerable<(Position, T)> Positions
        {
            get
            {
                foreach((Position pos, T item) in _grid)
                {
                    yield return (pos, item);
                }
            }
        }

        // Add Item
        // * Add to empty space
        // * Add to out of bounds space (invalid)
        // * Add to space with already existing item

        /// <summary>
        /// Attempts to add the specified <paramref name="item"/> into this <see cref="CraftingContainer"/>
        /// at the specified <paramref name="position"/>. 
        /// Returns true, if the item was added and false otherwise.
        /// </summary>
        public bool TryAddItem(Position position, T item)
        {
            return _grid.TryAdd(position, item); // returns false if position is already set
        }

        // Move Item in Container
        // * Move into empty space
        // * Move into occupied space
        // * Move into out of bounds space
        public bool Move(Position from, Position to)
        {
            return false;
        }

        // Remove Item
        // * Remove from empty space
        // * Remove from out of bounds
        // * Remove from spot with item
        public bool TryRemove(Position position, out T removed)
        {
            removed = default;
            return false;
        }

        public bool TryItemAt(Position position, out T result)
        {
            result = default;
            return false;
        }
        public T ItemAt(Position position)
        {
            return default;
        }

        public bool HasItemAt(Position position) => _grid.ContainsKey(position);

    }
}