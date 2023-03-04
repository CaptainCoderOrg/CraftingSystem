using UnityEngine;

[CreateAssetMenu(fileName = "CraftingCategory", menuName = "Crafting/Category", order = 2)]
public class CraftingCategoryData : ScriptableObject, ICraftingCategory
{
    [field: SerializeField]
    public string Name { get; private set; }
}
