using System.Collections.Generic;
using CaptainCoder.Core;
using UnityEngine;

namespace CaptainCoder.CraftingSystem.UnityEngine
{
    public class CraftingContainerData<T> : ScriptableObject, ICraftingContainer<T>.IAdapter
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [SerializeField]
        private int _rows;
        [SerializeField]
        private int _cols;
        [SerializeField]
        private CraftingCategoryData _craftingCategory;
        [SerializeField]
        private List<MutablePosition> _invalidPositions;

        #region CraftingContainerDelegate
        private ICraftingContainer<T> _delegate;
        public ICraftingContainer<T> CraftingContainer
        {
            get
            {
                if (_delegate == null)
                {
                    _delegate = new CraftingContainer<T>(
                        Name,
                        _rows,
                        _cols,
                        _craftingCategory,
                        _invalidPositions.Freeze());
                }
                return _delegate;
            }
            #endregion
        }
    }
}