using UnityEngine;
using UnityEngine.UIElements;
using Position = CaptainCoder.Core.Position;

public class CraftingContainerSlot : VisualElement
{
    public Image Icon { get; private set; }
    public bool IsInvalid { get; private set; }

    public Position Position { get; private set; }
    private ICraftingContainer<ItemData> _container;
    public CraftingContainerSlot(Position position, ICraftingContainer<ItemData> container, bool isInvalid = false)
    {
        Position = position;
        _container = container;
        IsInvalid = isInvalid;
        Icon = new Image();
        Add(Icon);
        Icon.AddToClassList("grid-slot-icon");
        if (isInvalid) { AddToClassList("grid-slot-container-invalid"); }
        else { AddToClassList("grid-slot-container-valid"); }
        AddToClassList("grid-slot-container");
        RegisterCallback<PointerDownEvent>(OnPointerDown);
    }

    public ItemData Item => _container.ItemAt(Position);

    // TODO: A better design would be to listen for changes on this position
    public void ForceUpdate()
    {
        if (_container.TryItemAt(Position, out ItemData result))
        {
            Icon.image = result.Sprite.texture;
        }
        else
        {
            Icon.image = null;
        }
    }

    public bool SetItem(ItemData item)
    {
        if (IsInvalid) { return false; }
        if (!_container.TryAddItem(Position, item)) { return false; } 
        Icon.image = item.Sprite.texture;
        return true;
    }

    public void ClearItem()
    {
        _container.TryRemove(Position, out _);
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        if (IsInvalid || evt.button != 0 || !_container.HasItemAt(Position))
        {
            return;
        }
        Icon.image = null;
        CraftingContainerUIController.StartDrag(evt.position, this);
    }
}
