using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftingContainerUIController : MonoBehaviour
{
    [SerializeField]
    private CraftingContainerData<ItemData> _craftingContainer;
    public ICraftingContainer<ItemData> CraftingContainer => _craftingContainer;
    [field: SerializeField]
    public ItemDatabase Database { get; private set; }
    public List<CraftingContainerSlot> InputGridSlots = new();
    public List<CraftingResultSlot> OutputGridSlots = new();
    private VisualElement _root;
    private VisualElement _slotContainer;
    private VisualElement _slotContainerParent;
    private static VisualElement _ghostIcon;
    private static bool _isDragging;
    private static CraftingContainerSlot _originalSlot;
    private Label _header;

    public void SetCraftingContainer(CraftingContainerData<ItemData> toSet)
    {
        _craftingContainer = toSet;
        _header.text = CraftingContainer.Name;
        BuildContainerGrid();
    }

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _slotContainer = _root.Q<VisualElement>("SlotContainer");
        _slotContainerParent = _root.Q<VisualElement>("SlotContainerParent");        
        _header = _root.Q<Label>("Header");
        _header.text = CraftingContainer.Name;
        _ghostIcon = _root.Q<VisualElement>("GhostIcon");

        BuildContainerGrid();
        BuildOutputGrid();
        var combineButton = _root.Q<Button>("CombineButton");
        combineButton.clicked += AttemptCombine;
        var clearButton = _root.Q<Button>("ClearButton");
        clearButton.clicked += () => {
            _craftingContainer.CraftingContainer.Clear();
            ForceUpdate();
        };
    }

    private void AttemptCombine()
    {
        IEnumerable<ItemData> items = CraftingContainer.Positions.Select(pair => pair.Item2);
        if (Database.RecipeDatabase.TryGetRecipe(items, CraftingContainer, out IShapelessRecipe<ItemData> recipe))
        {
            int ix = 0;
            foreach (ItemData item in recipe.Result)
            {
                Debug.Assert(ix < OutputGridSlots.Count);
                if (ix >= OutputGridSlots.Count) { break; }
                OutputGridSlots[ix].Item = item;
                ix++;
            }
            for (; ix < OutputGridSlots.Count; ix++)
            {
                OutputGridSlots[ix].ClearItem();
            }
            CraftingContainer.Clear();
            ForceUpdate();
        }
    }

    private void BuildOutputGrid()
    {
        var combineOutput = _root.Q<VisualElement>("CombineOutput");
        for (int i = 0; i < 4; i++)
        {
            CraftingResultSlot outputSlot = new();
            OutputGridSlots.Add(outputSlot);
            combineOutput.Add(outputSlot);
        }
    }

    private void BuildContainerGrid()
    {
        _slotContainer.Clear();
        InputGridSlots.Clear();
        HashSet<CaptainCoder.Core.Position> invalidPositions = CraftingContainer.InvalidPositions;
        for (int r = 0; r < CraftingContainer.Rows; r++)
        {
            GridRow row = new();
            _slotContainer.Add(row);
            for (int c = 0; c < CraftingContainer.Columns; c++)
            {
                CraftingContainerSlot slot = new((r, c), CraftingContainer, invalidPositions.Contains(new CaptainCoder.Core.Position(r, c)));
                InputGridSlots.Add(slot);
                row.Inner.Add(slot);
            }
        }
        ForceUpdate();


        _ghostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        _ghostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    public void ForceUpdate()
    {
        foreach (CraftingContainerSlot slot in InputGridSlots)
        {
            slot.ForceUpdate();
        }
    }

    public static void StartDrag(Vector2 position, CraftingContainerSlot originalSlot)
    {
        _isDragging = true;
        _originalSlot = originalSlot;
        _ghostIcon.style.top = position.y - _ghostIcon.layout.height / 2;
        _ghostIcon.style.left = position.x - _ghostIcon.layout.width / 2;
        _ghostIcon.style.backgroundImage = originalSlot.Item.Sprite.texture;
        _ghostIcon.style.visibility = Visibility.Visible;
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!_isDragging)
        {
            return;
        }
        _ghostIcon.style.top = evt.position.y - _ghostIcon.layout.height / 2;
        _ghostIcon.style.left = evt.position.x - _ghostIcon.layout.width / 2;
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!_isDragging)
        {
            return;
        }
        IEnumerable<CraftingContainerSlot> slots = InputGridSlots.Where(x =>
               x.worldBound.Overlaps(_ghostIcon.worldBound));
        if (slots.Count() > 0)
        {
            CraftingContainerSlot closestSlot = slots.OrderBy(x => Vector2.Distance
               (x.worldBound.position, _ghostIcon.worldBound.position)).First();
            if (CraftingContainer.TryMove(_originalSlot.Position, closestSlot.Position))
            {
                closestSlot.ForceUpdate();
                _originalSlot.ForceUpdate();
            }
            else
            {
                _originalSlot.Icon.image = _originalSlot.Item.Sprite.texture;
            }
        }
        else
        {
            _originalSlot.Icon.image = _originalSlot.Item.Sprite.texture;
        }
        _isDragging = false;
        _originalSlot = null;
        _ghostIcon.style.visibility = Visibility.Hidden;
    }
}