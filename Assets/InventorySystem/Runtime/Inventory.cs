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
        }


        private void SetupContainersFromChildren()
        {
            foreach (Container container in GetComponentsInChildren<Container>())
            {
                containers.Add(container);
            }
            containers.Sort();
            RenameAllSlots();
        }

        private void RenameAllSlots()
        {
            IterateSlot((Slot, SlotIndex) => Slot.RenameWithIndex(ref SlotIndex));
        }

        private void IterateSlot(Action<Slot, int> orderedSlot)
        {
            int SlotIndex = 0;
            foreach(Container container in containers)
            {
                foreach(Slot Slot in container.Slots)
                {
                    orderedSlot?.Invoke(Slot, SlotIndex);
                    SlotIndex++;
                }
            }
        }


        void Start()
        {

        }
    }
}
