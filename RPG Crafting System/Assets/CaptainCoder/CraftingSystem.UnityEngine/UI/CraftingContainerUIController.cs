using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CaptainCoder.CraftingSystem.UnityEngine
{
    public class CraftingContainerUIController : MonoBehaviour
    {
        public List<GridSlot> GridSlots = new ();
        private VisualElement m_Root;
        private VisualElement m_SlotContainer;
        private void Awake()
        {
            //Store the root from the UI Document component
            m_Root = GetComponent<UIDocument>().rootVisualElement;
            //Search the root for the SlotContainer Visual Element
            m_SlotContainer = m_Root.Q<VisualElement>("SlotContainer");
            //Create InventorySlots and add them as children to the SlotContainer
            for (int i = 0; i < 20; i++)
            {
                GridSlot item = new GridSlot();
                GridSlots.Add(item);
                m_SlotContainer.Add(item);
            }
        }
    }
}