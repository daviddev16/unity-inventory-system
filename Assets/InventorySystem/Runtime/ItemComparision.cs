using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public interface ItemComparision
    {
        bool IsSimilar(ItemStack ItemStack);

        bool IsSimilar(ItemStackHandler ItemStackHandler);
    }
}