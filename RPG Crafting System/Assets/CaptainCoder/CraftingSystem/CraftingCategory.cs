using System.Collections;
using System.Collections.Generic;
using System;
namespace CaptainCoder.CraftingSystem
{
    public readonly struct CraftingCategory
    {
        public readonly string Name;
        public CraftingCategory(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is CraftingCategory category &&
                   Name == category.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    } 
}