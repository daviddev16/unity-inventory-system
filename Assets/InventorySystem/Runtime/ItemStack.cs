using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public struct ItemStack
    {
        public bool Stackable { get; set; }
        public ItemStackBasedData ItemStackBasedData;
    }
}