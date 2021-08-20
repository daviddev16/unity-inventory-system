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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                AddItemStack(APPLE);
            }
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
                orderedSlot.Invoke(Slot, SlotIndex); return true;
            });
        }

        private ItemStackHandler FindItemStackHandler(ItemStack itemStack)
        {
            ItemStackHandler handler = null;
            IterateSlotsByFunction((Slot, SlotIndex) => 
            {
                if(!Slot.IsEmpty() && Slot.ContainsItem(itemStack))
                {
                    handler = Slot.GetItemHandler();
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
                ItemStackHandler handler = FindItemStackHandler(itemStack);
                if (handler != null && itemStack.Stackable)
                {
                    handler.ChangeAmount(true, 1);
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
