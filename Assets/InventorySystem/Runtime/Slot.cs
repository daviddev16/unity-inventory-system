using UnityEngine;
using System.Collections.Generic;

namespace InventorySys
{
    using Internals;
    public class Slot : MonoBehaviour, InventoryEntityState, IReceivable
    {
        private ItemStackHandler ItemHandler;
        private List<ASlotTracker> Trackers;

        public bool Trackable = false;
        public int SlotIndex = -1;

        private void Awake()
        {
            if(Trackable)
            {
                Trackers = new List<ASlotTracker>();
            }
        }

        /* create the item inside the slot */
        public void Populate(ItemStack itemStack, int initialAmount)
        {
            CreateItemHandler(new ItemStackHandlerInfo(itemStack, initialAmount));
            /* update state and trigger events */
            UpdateEntity();
            Received(itemStack);
        }

        public bool Set(ItemStackHandlerInfo itemInfo)
        {
            if(CanReceive(itemInfo.ItemStack))
            {
                ClearIfNecessary();
                CreateItemHandler(itemInfo);
                
                /* update state and trigger events */
                UpdateEntity();
                Received(itemInfo.ItemStack);
                return true;
            }
            return false;
        }

        private void CreateItemHandler(ItemStackHandlerInfo itemInfo)
        {
            ItemHandler = Instantiate(Inventory.GetInventory().ItemAsset,
                    transform.position, Quaternion.identity).GetComponent<ItemStackHandler>();

            /* setup itemHandler */
            ItemHandler.SetInformation(itemInfo);
            ItemHandler.transform.SetParent(transform);
            ItemHandler.ResolveTransform();
            ItemHandler.UpdateEntity();
        }

        public void UpdateEntity()
        {
            ItemHandler = GetComponentInChildren<ItemStackHandler>();
        }

        /* clear completely the current item if exists.*/
        private void ClearIfNecessary()
        {
            if(ItemHandler != null)
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

        /* add a new trigger */
        public void AddTracker(ASlotTracker tracker)
        {
            if(!Trackable)
            {
                Debug.LogWarning("This slot is not trackable.");
                return;
            }
            Trackers.Add(tracker);
        }

        public void Received(ItemStack itemStack)
        {
            if(!Trackable && Trackers != null) return;
            
            Trackers.ForEach(Tracker => 
                    Tracker.OnTrackStateEvent(new TrackEvent(this, itemStack)));
        }

        public void Absent()
        {
            if(!Trackable && Trackers != null) return;

            Trackers.ForEach(Tracker => 
                    Tracker.OnTrackStateEvent(new TrackEvent(this, null)));
        }

        public virtual bool CanReceive(ItemStack itemStack) { return true; }

    }
}