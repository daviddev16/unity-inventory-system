using System;
using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public class Slot : MonoBehaviour, InventoryEntityState
    {
        [SerializeField]
        private ItemStackHandler ItemStackHandler;

        /* create the item inside the slot */
        public void Populate(ItemStack ItemStack, int amount)
        {
            ItemStackHandler = Instantiate(ISA.GetItemStackHandlerAsset(),
                transform.position, Quaternion.identity).GetComponent<ItemStackHandler>();

            ItemStackHandler.UpdateHandlerInfo(ItemStack, amount);
            Setup(ItemStackHandler);
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
            Slot PreviousSlot = itemStackHandler.ParentSlot;
            if (IsEmpty())
            {
                Setup(itemStackHandler);
                UpdateSelfStageAnd(PreviousSlot);
            }
        }

        public void SwitchItemsBySlots(Slot Slot)
        {
            if (Slot.ItemStackHandler != null && ItemStackHandler != null)
            {
                Slot.Setup(ItemStackHandler);
                Setup(Slot.ItemStackHandler);

                UpdateSelfStageAnd(Slot);
            }
        }

        private void UpdateSelfStageAnd(Slot Slot)
        {
            Slot.UpdateStage();
            UpdateStage();
        }

        public void Setup(ItemStackHandler Handler)
        {
            Handler.SetSlotParent(this);
            Handler.ResolveTransform();
            Handler.UpdateStage();
        }

        public void UpdateStage()
        {
            ItemStackHandler = GetComponentInChildren<ItemStackHandler>();
        }
    }
}