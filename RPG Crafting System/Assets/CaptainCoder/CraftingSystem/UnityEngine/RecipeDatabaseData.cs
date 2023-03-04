using System.Collections.Generic;
using UnityEngine;

public class RecipeDatabaseData : ScriptableObject
{
    [SerializeField]
    private List<GameRecipeData> _recipes;
}
