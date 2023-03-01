
using UnityEngine;

namespace CaptainCoder.CraftingSystem.UnityEngine
{
    [CreateAssetMenu(fileName = "CraftingCategory", menuName = "Crafting/Category", order = 2)]
    public class CraftingCategoryData : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }

        public CraftingCategory AsStruct => new (Name);
    }
}