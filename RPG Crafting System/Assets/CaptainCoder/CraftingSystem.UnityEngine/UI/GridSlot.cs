using UnityEngine;
using UnityEngine.UIElements;

namespace CaptainCoder.CraftingSystem.UnityEngine
{
    public class GridSlot : VisualElement
    {
        [field: SerializeField]
        public Image Icon { get; private set; }
        [field: SerializeField]
        public string ItemGuid { get; private set; }= "";
        public GridSlot()
        {
            //Create a new Image element and add it to the root
            Icon = new Image();
            Add(Icon);
            //Add USS style properties to the elements
            Icon.AddToClassList("grid-slot-icon");
            AddToClassList("grid-slot-container");
        }
    }
}