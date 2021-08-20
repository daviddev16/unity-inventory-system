using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public struct ItemStackHandlerInfo 
    {
        /*item display*/
        public ItemStack ItemStack { get; set; }
        public int Amount { get; set; }
    }
}