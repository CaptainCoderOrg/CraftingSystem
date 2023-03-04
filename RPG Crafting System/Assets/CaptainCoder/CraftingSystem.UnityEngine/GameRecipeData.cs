using UnityEngine;

namespace CaptainCoder.CraftingSystem.UnityEngine
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Crafting/Recipe", order = 1)]
    public class GameRecipeData : ShapelessRecipeData<ItemData> {} 
}