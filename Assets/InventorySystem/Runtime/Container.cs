using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Container : MonoBehaviour
    {
        public Inventory Owner { protected set; get; }

        [SerializeField] public List<Slot> Slots { protected set; get; }
        [SerializeField] private int Order = 0;

        private void Awake()
        {
            Slots = new List<Slot>();
            SetupSlotsFromChildren();
        }

        public int GetOrder()
        {
            return Order;
        }

        private void SetupSlotsFromChildren()
        {
            foreach (Slot Slot in GetComponentsInChildren<Slot>()) Slots.Add(Slot);
        }
    }
}
