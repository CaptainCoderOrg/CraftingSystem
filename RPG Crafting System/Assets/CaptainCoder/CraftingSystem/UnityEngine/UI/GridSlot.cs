using UnityEngine;
using UnityEngine.UIElements;

public class GridSlot : VisualElement
{
    [field: SerializeField]
    public Image Icon { get; private set; }
    [field: SerializeField]
    public ItemData Item { get; private set; }
    public bool IsInvalid { get; private set; }
    public GridSlot(bool isInvalid = false)
    {
        IsInvalid = isInvalid;
        Icon = new Image();
        Add(Icon);
        Icon.AddToClassList("grid-slot-icon");
        if (isInvalid) { AddToClassList("grid-slot-container-invalid"); }
        else { AddToClassList("grid-slot-container-valid"); }
        AddToClassList("grid-slot-container");
        RegisterCallback<PointerDownEvent>(OnPointerDown);
    }

    public bool SetItem(ItemData item)
    {
        if (IsInvalid) { return false; }
        Icon.image = item.Sprite.texture;
        Item = item;
        return true;
    }

    public void ClearItem()
    {
        Icon.image = null;
        Item = null;
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        if (IsInvalid || evt.button != 0 || Item == null)
        {
            return;
        }
        Icon.image = null;
        CraftingContainerUIController.StartDrag(evt.position, this);
    }
}
