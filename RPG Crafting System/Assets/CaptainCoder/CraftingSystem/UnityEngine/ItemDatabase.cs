using UnityEngine;
using System.Collections.Generic;

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
    [field: SerializeField]
    public List<GameRecipeData> Recipes { get; private set; }
    public RecipeDatabase<ItemData> RecipeDatabase { get; private set; }

    public void OnEnable()
    {
        if (RecipeDatabase == null)
        {
            RecipeDatabase = new RecipeDatabase<ItemData>(Recipes);
        }
    }
}

