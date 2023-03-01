using UnityEngine;

namespace CaptainCoder.CraftingSystem.UnityEngine
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "Crafting/Item", order = 0)]
    public class GameItem : ScriptableObject, IItem
    {
        [SerializeField]
        private string _name;
        public string Name => _name;
    }
}
