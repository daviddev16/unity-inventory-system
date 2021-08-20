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

        public void UpdateState()
        {
            ItemStackHandler = GetComponentInChildren<ItemStackHandler>();
        }

        internal void Migrate(ItemStackHandler itemStackHandler)
        {
            Slot PreviousSlot = itemStackHandler.GetComponentInParent<Slot>(); 
            if (IsEmpty())
            {
                itemStackHandler.SetSlotParent(this);
                itemStackHandler.ResolveTransform();
                ItemStackHandler = itemStackHandler;
                PreviousSlot.UpdateState();
            }
        }

        public void SwitchItemsFromSlots(Slot Slot)
        {
            if(Slot.ItemStackHandler != null && ItemStackHandler != null)
            {
                ItemStackHandler.SetSlotParent(Slot);
                ItemStackHandler.ResolveTransform();

                Slot.ItemStackHandler.SetSlotParent(this);
                Slot.ItemStackHandler.ResolveTransform();

                Slot.UpdateState();
                UpdateState();

            }

        }
    }
}