using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace CaptainCoder.CraftingSystem.UnityEngine
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Crafting/Item Database", order = 4)]
    public class ItemDatabase : ScriptableObject
    {
        [field: SerializeField]
        public ItemData Boat { get; private set; }
        [field: SerializeField]
        public ItemData Rope { get; private set; }
        [field: SerializeField]
        public ItemData Wood { get; private set; }
    }
}
