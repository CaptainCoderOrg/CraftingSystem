using System.Collections.Generic;
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
    public List<GridSlot> InputGridSlots = new();
    public List<GridSlot> OutputGridSlots = new();
    private VisualElement m_Root;
    private VisualElement m_SlotContainer;
    private static VisualElement m_GhostIcon;
    private static bool m_IsDragging;
    private static GridSlot m_OriginalSlot;


    private void Awake()
    {
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_SlotContainer = m_Root.Q<VisualElement>("SlotContainer");
        var header = m_Root.Q<Label>("Header");
        header.text = CraftingContainer.Name;
        m_GhostIcon = m_Root.Q<VisualElement>("GhostIcon");

        BuildContainerGrid();
        BuildOutputGrid();
        var combineButton = m_Root.Q<Button>("CombineButton");
        combineButton.clicked += AttemptCombine;
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
                OutputGridSlots[ix].SetItem(item);
                ix++;
            }
            for (; ix < OutputGridSlots.Count; ix++)
            {
                OutputGridSlots[ix].ClearItem();
            }
        }
    }

    private void BuildOutputGrid()
    {
        var combineOutput = m_Root.Q<VisualElement>("CombineOutput");
        for (int i = 0; i < 4; i++)
        {
            GridSlot outputSlot = new();
            OutputGridSlots.Add(outputSlot);
            combineOutput.Add(outputSlot);
        }
    }

    private void BuildContainerGrid()
    {
        HashSet<CaptainCoder.Core.Position> invalidPositions = CraftingContainer.InvalidPositions;
        for (int r = 0; r < CraftingContainer.Rows; r++)
        {
            GridRow row = new();
            m_SlotContainer.Add(row);
            for (int c = 0; c < CraftingContainer.Columns; c++)
            {
                GridSlot slot = invalidPositions.Contains(new CaptainCoder.Core.Position(r, c)) ? new GridSlot(true) : new GridSlot();
                InputGridSlots.Add(slot);
                row.Inner.Add(slot);
            }
        }

        CraftingContainer.TryAddItem((1,0), Database.Boat);
        InputGridSlots[1].SetItem(Database.Wood);
        InputGridSlots[2].SetItem(Database.Wood);
        InputGridSlots[5].SetItem(Database.Wood);
        InputGridSlots[6].SetItem(Database.Rope);

        m_GhostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        m_GhostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    public static void StartDrag(Vector2 position, GridSlot originalSlot)
    {
        m_IsDragging = true;
        m_OriginalSlot = originalSlot;
        m_GhostIcon.style.top = position.y - m_GhostIcon.layout.height / 2;
        m_GhostIcon.style.left = position.x - m_GhostIcon.layout.width / 2;
        m_GhostIcon.style.backgroundImage = originalSlot.Item.Sprite.texture;
        m_GhostIcon.style.visibility = Visibility.Visible;
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!m_IsDragging)
        {
            return;
        }
        m_GhostIcon.style.top = evt.position.y - m_GhostIcon.layout.height / 2;
        m_GhostIcon.style.left = evt.position.x - m_GhostIcon.layout.width / 2;
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!m_IsDragging)
        {
            return;
        }
        IEnumerable<GridSlot> slots = InputGridSlots.Where(x =>
               x.worldBound.Overlaps(m_GhostIcon.worldBound));
        if (slots.Count() > 0)
        {
            GridSlot closestSlot = slots.OrderBy(x => Vector2.Distance
               (x.worldBound.position, m_GhostIcon.worldBound.position)).First();
            closestSlot.SetItem(m_OriginalSlot.Item);
            m_OriginalSlot.ClearItem();
        }
        else
        {
            m_OriginalSlot.Icon.image = m_OriginalSlot.Item.Sprite.texture;
        }
        m_IsDragging = false;
        m_OriginalSlot = null;
        m_GhostIcon.style.visibility = Visibility.Hidden;
    }
}