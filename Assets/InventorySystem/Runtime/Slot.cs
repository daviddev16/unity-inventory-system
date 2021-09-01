using UnityEngine;
using InventorySystem.Internals;

namespace InventorySystem
{
    public class Slot : MonoBehaviour, InventoryEntityState, IReceivable
    {
        [SerializeField]
        private ItemStackHandler ItemHandler;

        /* create the item inside the slot */
        public void Populate(ItemStack itemStack, int initialAmount)
        {
            ItemHandler = Instantiate(Inventory.GetInventory().ItemStackHandlerAsset,
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
            ItemHandler = Instantiate(Inventory.GetInventory().ItemStackHandlerAsset,
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