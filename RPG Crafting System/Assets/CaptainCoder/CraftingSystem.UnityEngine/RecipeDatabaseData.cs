using System.Collections.Generic;
using UnityEngine;

namespace CaptainCoder.CraftingSystem.UnityEngine
{
    public class RecipeDatabaseData : ScriptableObject
    {
        [SerializeField]
        private List<GameRecipeData> _recipes;
        
    }
}