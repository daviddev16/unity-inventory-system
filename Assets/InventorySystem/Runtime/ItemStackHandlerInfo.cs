using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public struct ItemStackHandlerInfo : System.ICloneable
    {
        /*item display*/
        public ItemStack ItemStack { get; set; }
        public int Amount { get; set; }

        public ItemStackHandlerInfo(ItemStack ItemStack, int Amount)
        {
            this.ItemStack = ItemStack;
            this.Amount = Amount;
        }

        public object Clone()
        {
            return new ItemStackHandlerInfo(ItemStack, Amount);
        }
    }
}