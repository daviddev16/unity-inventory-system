using UnityEngine;
using System.Collections.Generic;
using System;

namespace InventorySys
{
    public sealed class Inventory : MonoBehaviour
    {
        private static Inventory InventoryInstance;

        [SerializeField] private List<Container> containers;
        public GameObject ItemAsset;
        [SerializeField] public float DragSpeed { get; set; } = 2f;

        public static Inventory GetInventory()
        {
            return InventoryInstance;
        }

        private void Awake()
        {
            if (InventoryInstance == null)
            {
                InventoryInstance = this;
            }
            else
            {
                Destroy(this);
            }

            SetupContainersFromChildren();
            RenameAllSlots();
        }

        public void RemoveItemStack(ItemStack itemStack)
        {
            ItemStackHandler ItemHandler = null;
            if (ContainsItem(itemStack, out ItemHandler))
            {
                if (itemStack.Stackable)
                {
                    ItemHandler.ChangeAmount(false, 1);
                }
                else
                {
                    ItemHandler.SelfPurge();
                }
            }
        }

        public void AddItemStack(ItemStack itemStack, int Amount)
        {
            for (int i = 0; i < Amount; i++)
            {
                AddItemStack(itemStack);
            }
        }

        public void Set(Internals.ItemStackHandlerInfo information, Slot slot)
        {
            if(slot != null)
            {
                slot.Set(information);
            }
        }


        public bool ContainsItem(ItemStack itemStack)
        {
            foreach (Container container in containers)
            {
                foreach (Slot Slot in container.Slots)
                {
                    if (Slot.GetItemHandler() != null && Slot.GetItemHandler().IsSimilar(itemStack, ItemStack.HIGH_LEVEL_COMPARISON))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ContainsItem(ItemStack itemStack, out ItemStackHandler ItemHandler)
        {
            foreach (Container container in containers)
            {
                foreach (Slot Slot in container.Slots)
                {
                    if (Slot.GetItemHandler() != null && Slot.GetItemHandler().IsSimilar(itemStack, ItemStack.HIGH_LEVEL_COMPARISON))
                    {
                        ItemHandler = Slot.GetItemHandler();
                        return true;
                    }
                }
            }

            ItemHandler = null;
            return false;
        }

        public void IterateSlots(Func<Slot, bool> SlotFunc)
        {
            foreach (Container container in containers)
            {
                foreach (Slot Slot in container.Slots)
                {
                    if (SlotFunc.Invoke(Slot))
                    {
                        return;
                    }
                }
            }
        }

        public void AddItemStack(ItemStack itemStack)
        {
            ItemStackHandler ItemHandler = null;
            if (itemStack.Stackable)
            {
                if (ContainsItem(itemStack, out ItemHandler))
                {
                    ItemHandler.ChangeAmount(true, 1);
                    return;
                }
                else
                {
                    IterateSlots(Slot =>
                    {
                        if (Slot.IsEmpty() && Slot.CanReceive(itemStack))
                        {
                            Slot.Populate(itemStack, 1);
                            return true;
                        }
                        return false;
                    });
                }
            }
            else
            {
                IterateSlots(Slot =>
                {
                    if (Slot.IsEmpty())
                    {
                        Slot.Populate(itemStack, 1);
                        return true;
                    }
                    return false;
                });
            }
        }

        public bool IsFull()
        {
            bool IsFull = true;
            IterateSlots(Slot =>
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
                if (container.IsExcluded())
                {
                    containers.Add(container);
                }
            }
            containers.Sort(Internals.ContainerComparer.CONTAINER_COMPARER);
        }

        public Slot GetSlot(int slotIndex)
        {
            Slot SelectedSlot = null;
            IterateSlots((Slot) => {
                if(Slot.SlotIndex == slotIndex)
                {
                    SelectedSlot = Slot;
                    return true;
                }
                return false;
            });
            return SelectedSlot;
        }

        private void RenameAllSlots()
        {
            int SlotIndex = 0;
            foreach (Container container in containers)
            {
                foreach (Slot Slot in container.Slots)
                {
                    Slot.gameObject.name = "Slot " + SlotIndex;
                    Slot.SlotIndex = SlotIndex;
                    SlotIndex++;
                }
            }
        }
    }
}
