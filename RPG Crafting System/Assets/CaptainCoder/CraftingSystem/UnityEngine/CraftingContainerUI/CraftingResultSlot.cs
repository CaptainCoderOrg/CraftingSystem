using UnityEngine;
using UnityEngine.UIElements;

public class CraftingResultSlot : VisualElement
{
    public Image Icon { get; private set; }
    private ItemData _item;
    public ItemData Item 
    { 
        get => _item; 
        set
        {
            _item = value;
            Icon.image = _item?.Sprite.texture;
        }
    }
    public CraftingResultSlot()
    {
        Icon = new Image();
        Add(Icon);
        Icon.AddToClassList("grid-slot-icon");
        AddToClassList("grid-slot-container");
    }

    public void ClearItem() => Item = null;

}
