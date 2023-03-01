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
            : this(rows, columns, new[] { category }, invalidPositions) { }

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
                foreach ((Position pos, T item) in _grid)
                {
                    yield return (pos, item);
                }
            }
        }

        /// <summary>
        /// Attempts to add the specified <paramref name="item"/> into this <see cref="CraftingContainer"/>
        /// at the specified <paramref name="position"/>. 
        /// Returns true, if the item was added and false otherwise.
        /// </summary>
        public bool TryAddItem(Position position, T item)
        {
            if (position.Row < 0 || position.Col < 0 || position.Row >= Rows || position.Col >= Columns) { return false; }
            if (InvalidPositions.Contains(position)) { return false; }
            return _grid.TryAdd(position, item); // returns false if position is already set
        }

        public bool TryMove(Position from, Position to)
        {
            // At least ONE position must have something
            if (!_grid.ContainsKey(from) && !_grid.ContainsKey(to)) { return false; }
            // At this point, at least ONE position is occupied

            // If both spots are occupied, swap them
            if (_grid.ContainsKey(from) && _grid.ContainsKey(to))
            {
                (_grid[to], _grid[from]) = (_grid[from], _grid[to]);
            }
            else if (_grid.ContainsKey(from))
            {
                _grid[to] = _grid[from];
                _grid.Remove(from);
            }
            else // if (_grid.ContainsKey(to))
            {
                _grid[from] = _grid[to];
                _grid.Remove(to);
            }
            return true;
        }

        public bool TryRemove(Position position, out T removed)
        {
            if (_grid.TryGetValue(position, out removed))
            {
                _grid.Remove(position);
                return true;
            }
            return false;
        }

        public bool TryItemAt(Position position, out T result) => _grid.TryGetValue(position, out result);
        public T ItemAt(Position position) => _grid[position];
        public bool HasItemAt(Position position) => _grid.ContainsKey(position);
    }
}