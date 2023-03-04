 #if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
    [field: SerializeField]
    public bool ScanForRecipes { get; private set; }

    

    public void OnEnable()
    {
        if (RecipeDatabase == null)
        {
            RecipeDatabase = new RecipeDatabase<ItemData>(Recipes);
        }
    }

     #if UNITY_EDITOR
    public void OnValidate()
    {
        if (ScanForRecipes)
        {
            string[] pathEl = AssetDatabase.GetAssetPath(this).Split("/");
            string path = string.Join("/", pathEl.Take(pathEl.Length - 1));
            Recipes = AssetDatabase
                .FindAssets($"t:{nameof(GameRecipeData)}", new[]{path})
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<GameRecipeData>).ToList();
            ScanForRecipes = false;
        }
    }
    #endif
}

