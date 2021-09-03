using UnityEngine;

namespace InventorySys
{
    using Internals;
    public class Slot : MonoBehaviour, InventoryEntityState, IReceivable
    {
        private ItemStackHandler ItemHandler;
        public int SlotIndex = -1;

        /* create the item inside the slot */
        public void Populate(ItemStack itemStack, int initialAmount)
        {
            ItemHandler = Instantiate(Inventory.GetInventory().ItemAsset,
                transform.position, Quaternion.identity).GetComponent<ItemStackHandler>();

            ItemHandler.SetInformation(new ItemStackHandlerInfo(itemStack, initialAmount));

            ItemHandler.transform.SetParent(transform);
            ItemHandler.ResolveTransform();
            ItemHandler.UpdateEntity();
            
            UpdateEntity();
        }
        
        public void Set(ItemStackHandlerInfo ItemInfo)
        {
            ClearIfNecessary();
            ItemHandler = Instantiate(Inventory.GetInventory().ItemAsset,
                transform.position, Quaternion.identity).GetComponent<ItemStackHandler>();

            ItemHandler.SetInformation(ItemInfo);

            ItemHandler.transform.SetParent(transform);
            ItemHandler.ResolveTransform();
            ItemHandler.UpdateEntity();

            UpdateEntity();
        }

        public void UpdateEntity()
        {
            ItemHandler = GetComponentInChildren<ItemStackHandler>();
        }

        /* slot filter*/
        public virtual bool CanReceive(ItemStack itemStack)
        {
            return true;
        }

        private void ClearIfNecessary()
        {
            if (ItemHandler != null)
            {
                ItemHandler.SelfPurge();
                UpdateEntity();
            }
        }

        public bool IsEmpty()
        {
            return ItemHandler == null;
        }

        public ItemStackHandler GetItemHandler()
        {
            return ItemHandler;
        }
    }
}