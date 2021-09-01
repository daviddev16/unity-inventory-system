using UnityEngine;
using System;
using System.Collections.Generic;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {

        private static Inventory Instance;

        [SerializeField]
        private List<Container> containers;

        public GameObject ItemStackHandlerAsset;

        public ItemStack[] RegisteredItemStacks;

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
                AddItemStack(RegisteredItemStacks[UnityEngine.Random.Range(0, RegisteredItemStacks.Length)]);
            }
        }

        private void Start()
        {
            foreach (ItemStack itemStack in RegisteredItemStacks)
            {
                AddItemStack(itemStack);
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
                    if (Slot.GetItemHandler() != null && Slot.GetItemHandler().IsSimilar(itemStack))
                    {
                        ItemHandler = Slot.GetItemHandler();
                        return true;
                    }
                }
            }

            ItemHandler = null;

            return false;
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
                    foreach (Container container in containers)
                    {
                        foreach (Slot Slot in container.Slots)
                        {
                            if (Slot.IsEmpty() && Slot.CanReceive(itemStack))
                            {
                                Slot.Populate(itemStack, 1);
                                return;
                            }
                        }
                    }
                }
            }

            else
            {
                foreach (Container container in containers)
                {
                    foreach (Slot Slot in container.Slots)
                    {
                        if (Slot.IsEmpty())
                        {
                            Slot.Populate(itemStack, 1);
                            return;
                        }
                    }
                }
            }
        }

        public bool IsFull()
        {

            foreach (Container container in containers)
            {
                foreach (Slot Slot in container.Slots)
                {
                    if (Slot.IsEmpty())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void SetupContainersFromChildren()
        {
            foreach (Container container in GetComponentsInChildren<Container>())
            {
                containers.Add(container);
            }
            containers.Sort(ContainerComparer.CONTAINER_COMPARER);
        }

        private void RenameAllSlots()
        {
            int SlotIndex = 0;
            foreach (Container container in containers)
            {
                foreach (Slot Slot in container.Slots)
                {
                    SlotIndex++;
                    Slot.RenameSlot(SlotIndex);
                }
            }
        }
    }
}
