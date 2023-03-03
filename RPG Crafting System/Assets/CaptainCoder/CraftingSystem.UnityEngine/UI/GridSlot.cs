using UnityEngine;
using UnityEngine.UIElements;

namespace CaptainCoder.CraftingSystem.UnityEngine
{
    public class GridSlot : VisualElement
    {
        [field: SerializeField]
        public Image Icon { get; private set; }
        [field: SerializeField]
        public ItemData Item { get; private set; }
        public GridSlot()
        {
            //Create a new Image element and add it to the root
            Icon = new Image();
            Add(Icon);
            //Add USS style properties to the elements
            Icon.AddToClassList("grid-slot-icon");
            AddToClassList("grid-slot-container");
            RegisterCallback<PointerDownEvent>(OnPointerDown);
        }

        public void SetItem(ItemData item)
        {
            Icon.image = item.Sprite.texture;
            Item = item;
        }

        public void ClearItem()
        {
            Icon.image = null;
            Item = null;
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            //Not the left mouse button
            if (evt.button != 0 || Item == null)
            {
                return;
            }
            //Clear the image
            Icon.image = null;
            //Start the drag
            CraftingContainerUIController.StartDrag(evt.position, this);
        }
    }
}