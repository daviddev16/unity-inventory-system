using System;
using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public class Slot : MonoBehaviour
    {
        [SerializeField]
        private ItemStackHandler ItemStackHandler;

        /* create the item inside the slot */
        public void Populate(ItemStack ItemStack, int amount)
        {
            ItemStackHandler = Instantiate(ISA.GetItemStackHandlerAsset(), 
                transform.position, Quaternion.identity).GetComponent<ItemStackHandler>();

            ItemStackHandler.SetSlotParent(this);
            ItemStackHandler.ResolveTransform();

            ItemStackHandler.UpdateHandlerInfo(ItemStack, amount);
        }

        public bool ContainsItem(ItemStack ItemStack)
        {
            return GetItemHandler() != null && GetItemHandler().IsSimilar(ItemStack);
        }

        public ItemStackHandler GetItemHandler()
        {
            return ItemStackHandler;
        }

        internal protected void RenameSlot(ref int SlotIndex)
        {
            gameObject.name = "Slot " + SlotIndex;
        }

        public bool IsEmpty()
        {
            return ItemStackHandler == null;
        }

        internal void Migrate(ItemStackHandler itemStackHandler)
        {
            if (IsEmpty())
            {
                itemStackHandler.SetSlotParent(this);
                itemStackHandler.ResolveTransform();
                ItemStackHandler = itemStackHandler;
            }
        }
    }
}