
namespace CaptainCoder.CraftingSystem
{

    public class Item : IItem
    {

        public Item(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

}