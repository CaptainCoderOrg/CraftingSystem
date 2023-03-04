namespace CaptainCoder.CraftingSystem
{
    public class SimpleItem : IItem
    {
        public SimpleItem(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}