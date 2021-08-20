using UnityEngine;
using System;
using System.Collections.Generic;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {

        [SerializeField]
        private List<Container> containers;

        private void Awake()
        {
            SetupContainersFromChildren();
            RenameAllSlots();
        }

        public static ItemStack APPLE = new ItemStack(false, null);

        void Start()
        {
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                AddItemStack(APPLE);
            }
        }

        private void SetupContainersFromChildren()
        {
            foreach (Container container in GetComponentsInChildren<Container>())
            {
                containers.Add(container);
            }
            containers.Sort(new ContainerComparer());
        }

        private void RenameAllSlots()
        {
            IterateSlots((Slot, SlotIndex) =>
            {
                Slot.RenameWithIndex(ref SlotIndex);
                return false;
            });
        }

        /*returns true if you want to break the iteration */
        private void IterateSlots(Func<Slot, int, bool> orderedSlot)
        {
            int SlotIndex = 0;
            bool broke = false;
            foreach (Container container in containers)
            {
                if (broke)
                {
                    break;
                }
                foreach (Slot Slot in container.Slots)
                {
                    if (orderedSlot.Invoke(Slot, SlotIndex))
                    {
                        broke = true;
                        break;
                    }
                    SlotIndex++;
                }
            }
        }

        public ItemStackHandler ContainsItemStack(ItemStack itemStack)
        {
            ItemStackHandler handler = null;
            IterateSlots((Slot, SlotIndex) => {
                if(!Slot.IsEmpty() && Slot.ItemStackHandler.ItemInfo.ItemStack.Equals(itemStack))
                {
                    handler = Slot.ItemStackHandler;
                    return false;
                }
                return true;
            });
            return handler;
        }

        public void AddItemStack(ItemStack itemStack)
        {
            IterateSlots((Slot, SlotIndex) =>
            {
                ItemStackHandler handler = ContainsItemStack(itemStack);
                if (handler != null && itemStack.Stackable)
                {
                    handler.ItemInfo.Amount += 1;
                    handler.UpdateStates();
                    return true;
                }
                else if (Slot.IsEmpty())
                {
                    Slot.Populate(itemStack, 1);
                    return true;
                }
                return false;
            });
        }

        
    }
}
