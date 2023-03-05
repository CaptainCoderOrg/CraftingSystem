using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class FakeInventorySelector : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> _items;
    [SerializeField]
    private CraftingContainerUIController _craftingController;
    private VisualElement _root;
    private VisualElement _container;


    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        // // _root.style.opacity = 0;
        _container = _root.Q<VisualElement>("Container");

        foreach (ItemData item in _items)
        {
            Button button = new()
            {
                text = item.Name
            };
            _container.Add(button);
            button.clicked += () =>
            {
                for (int r = 0; r < _craftingController.CraftingContainer.Rows; r++)
                {
                    for (int c = 0; c < _craftingController.CraftingContainer.Columns; c++)
                    {
                        if(_craftingController.CraftingContainer.TryAddItem((r, c), item)) 
                        { 
                            _craftingController.ForceUpdate();
                            return; 
                        }
                    }
                }
                
            };
            button.SetEnabled(true);
        }
    }
}