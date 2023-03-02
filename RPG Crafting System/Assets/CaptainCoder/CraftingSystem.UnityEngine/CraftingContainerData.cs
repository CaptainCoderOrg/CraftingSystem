using System.Collections.Generic;
using CaptainCoder.Core;
using UnityEngine;
using System.Linq;

namespace CaptainCoder.CraftingSystem.UnityEngine
{
    public class CraftingContainerData<T> : ScriptableObject, ICraftingContainer<T> where T : IItem
    {
        private ICraftingContainer<T> _delegate;
        private ICraftingContainer<T> Delegate
        {
            get
            {
                if (_delegate == null)
                {
                    _delegate = new CraftingContainer<T>(
                        _rows, 
                        _cols, 
                        _craftingCategory, 
                        _invalidPositions.Select(pos => new Position(pos.Row, pos.Col)));
                }
                return _delegate;
            }
        }

        [field: SerializeField]
        public string Name { get; private set; }
        [SerializeField]
        private int _rows;
        [SerializeField]
        private int _cols;
        [SerializeField]
        private CraftingCategoryData _craftingCategory;
        [SerializeField]
        private List<InvalidPosition> _invalidPositions;
        public int Rows => Delegate.Rows;
        public int Columns => Delegate.Columns;
        public HashSet<ICraftingCategory> Categories => Delegate.Categories;
        public HashSet<Position> InvalidPositions => Delegate.InvalidPositions;
        public IEnumerable<(Position, T)> Positions => Delegate.Positions;
        public bool HasItemAt(Position position) => Delegate.HasItemAt(position);
        public T ItemAt(Position position) => Delegate.ItemAt(position);
        public bool TryAddItem(Position position, T item) => Delegate.TryAddItem(position, item);
        public bool TryItemAt(Position position, out T result) => Delegate.TryItemAt(position, out result);
        public bool TryMove(Position from, Position to) => Delegate.TryMove(from, to);
        public bool TryRemove(Position position, out T removed) => Delegate.TryRemove(position, out removed);

        [System.Serializable]
        internal struct InvalidPosition
        {
            public int Row;
            public int Col;
        }
    }


}