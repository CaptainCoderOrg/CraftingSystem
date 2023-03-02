using System;
namespace CaptainCoder.CraftingSystem
{
    public readonly struct CraftingCategory : ICraftingCategory
    {
        public string Name { get; }
        public CraftingCategory(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is CraftingCategory category &&
                   Name == category.Name;
        }

        public override int GetHashCode() => HashCode.Combine(Name);
    } 
}