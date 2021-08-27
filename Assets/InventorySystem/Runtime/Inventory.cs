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

        private void Update()
        {
           
        }

        private void IterateSlotsByFunction(Func<Slot, int, bool> orderedSlot)
        {
            int SlotIndex = 0;
            bool breakContainersLoop = false;
            foreach (Container container in containers)
            {
                if (breakContainersLoop)
                {
                    break;
                }
                foreach (Slot Slot in container.Slots)
                {
                    if (orderedSlot.Invoke(Slot, SlotIndex))
                    {
                        breakContainersLoop = true;
                        break;
                    }
                    SlotIndex++;
                }
            }
        }

        private void IterateSlots(Action<Slot, int> orderedSlot)
        {
            IterateSlotsByFunction((Slot, SlotIndex) =>
            {
                orderedSlot.Invoke(Slot, SlotIndex); return false;
            });
        }

        private ItemStackHandler FindItemStackHandler(ItemStack itemStack)
        {
            ItemStackHandler handler = null;
            IterateSlotsByFunction((Slot, SlotIndex) => 
            {
                if(Slot.ContainsItem(itemStack))
                {
                    handler = Slot.GetItemHandler();
                    return false;
                }
                return true;
            });
            
            return handler;
        }

        public void RemoveItemStack(ItemStack itemStack)
        {
            ItemStackHandler ItemStackHandler = FindItemStackHandler(itemStack);
            if (ItemStackHandler != null)
            {
                if (ItemStackHandler.GetItemStack().Stackable)
                {
                    ItemStackHandler.ChangeAmount(false, 1);
                }
                else
                {
                    ItemStackHandler.SelfPurge();
                }
            }
        }

        public void AddItemStack(ItemStack itemStack, int Amount)
        {
            for(int i = 0; i < Amount; i++)
            {
                AddItemStack(itemStack);
            }
        }

        public void AddItemStack(ItemStack itemStack)
        {
            IterateSlotsByFunction((Slot, SlotIndex) =>
            {
                ItemStackHandler ItemStackHandler = Slot.GetItemHandler();

                if(ItemStackHandler != null)
                {
                    if(ItemStackHandler.IsSimilar(itemStack) && ItemStackHandler.IsFree())
                    {
                        ItemStackHandler.ChangeAmount(true, 1);
                        return true;

                    }
                }
                else if (Slot.IsEmpty())
                {
                    Slot.Populate(itemStack, 1);
                    return true;
                }

                /*just skip to the next slot*/
                return false;
            });
        }

        public bool IsFull()
        {
            bool IsFull = true;
            IterateSlotsByFunction((Slot, SlotIndex) => 
            {
                if (Slot.IsEmpty())
                {
                    IsFull = false;
                    return true;
                }

                return false;
            });
            return IsFull;
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
            IterateSlots((Slot, SlotIndex) => Slot.RenameSlot(ref SlotIndex));
        }
    }
}
