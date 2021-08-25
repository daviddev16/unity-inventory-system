using System.Collections.Generic;
using UnityEngine;


namespace InventorySystem
{
    [System.Serializable]
    public class ItemStack
    {
        public bool Stackable { get; }
        public int StackLimit { get; }

        public ItemStack(bool Stackable, int StackLimit)
        {
            this.Stackable = Stackable;
            this.StackLimit = StackLimit;
        }

    }
}