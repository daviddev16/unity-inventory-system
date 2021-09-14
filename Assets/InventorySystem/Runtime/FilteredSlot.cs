using System.Collections;
using UnityEngine;

namespace InventorySys
{
    public class FilteredSlot : Slot
    {
        public int AllowedID = -1;
        public byte Data = 0;

        public override bool CanReceive(ItemStack itemStack)
        {
            return (itemStack.ID == AllowedID && itemStack.Data == Data) || (AllowedID == -1);
        }

    }
}