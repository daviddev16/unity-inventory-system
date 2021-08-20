using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public struct ItemStack
    {
        public bool Stackable { get; set; }
        public ItemStackBasedData[] ItemStackBasedData { get; set; }
        public int StackLimit { get; set; }

        public ItemStack(bool Stackable, int StackLimit, ItemStackBasedData[] ItemStackBasedData)
        {
            this.ItemStackBasedData = ItemStackBasedData;
            this.Stackable = Stackable;
            this.StackLimit = StackLimit;
        }


    }
}