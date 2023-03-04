using UnityEngine;
using UnityEngine.UIElements;

public class GridRow : VisualElement
{
    public GridRowInner Inner { get; } = new ();
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
   }

}
