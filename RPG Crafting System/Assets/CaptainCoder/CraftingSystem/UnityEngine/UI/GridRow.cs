using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class GridRow : VisualElement
{
    public GridRowInner Inner { get; } = new();
    public float Height => resolvedStyle.height;
    public GridRow()
    {
        AddToClassList("grid-row");
        Add(Inner);
    }

    public class GridRowInner : VisualElement
    {
        public GridRowInner()
        {
            AddToClassList("grid-row-inner");
            
        }

        public float Width => resolvedStyle.width;

        
    }

}
