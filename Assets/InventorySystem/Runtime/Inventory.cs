using UnityEngine;
using System.Collections.Generic;
using System;

namespace InventorySystem
{
    public sealed class Inventory : MonoBehaviour
    {
        private static Inventory Instance;

        [SerializeField] private List<Container> containers;
        public GameObject ItemStackHandlerAsset;

        [SerializeField]
        private ItemStack[] RegisteredItemStack;

        public static Inventory GetInventory()
        {
            return Instance;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            SetupContainersFromChildren();
            RenameAllSlots();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                AddItemStack(RegisteredItemStack[UnityEngine.Random.Range(0, RegisteredItemStack.Length)]);
            }
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
                containers.Add(container);
            }
            containers.Sort(Internals.ContainerComparer.CONTAINER_COMPARER);
        }

        private void RenameAllSlots()
        {
            int SlotIndex = 0;
            foreach (Container container in containers)
            {
                foreach (Slot Slot in container.Slots)
                {
                    SlotIndex++;
                    Slot.gameObject.name = "Slot " + SlotIndex;
                }
            }
        }
    }
}
