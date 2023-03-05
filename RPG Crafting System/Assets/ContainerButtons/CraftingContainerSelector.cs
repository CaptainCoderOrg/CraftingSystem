using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftingContainerSelector : MonoBehaviour
{
    [SerializeField]
    private List<GameCraftingContainerData> _craftingContainers;
    [SerializeField]
    private CraftingContainerUIController _craftingController;
    private VisualElement _root;
    private VisualElement _container;


    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        // // _root.style.opacity = 0;
        _container = _root.Q<VisualElement>("Container");

        foreach (GameCraftingContainerData container in _craftingContainers)
        {
            Button button = new()
            {
                text = container.Name
            };
            _container.Add(button);
            button.clicked += () => _craftingController.SetCraftingContainer(container);
            button.SetEnabled(true);
        }
    }
}