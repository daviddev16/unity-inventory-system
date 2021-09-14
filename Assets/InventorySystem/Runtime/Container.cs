using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySys
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Container : MonoBehaviour
    {
        public Inventory Owner { protected set; get; }

        public List<Slot> Slots { protected set; get; }

        [SerializeField] private bool Exclude = true;
        [SerializeField] private bool SetupChildrens = true;
        [SerializeField] private int Order = 0;

        private void Awake()
        {
            GetComponent<CanvasGroup>().ignoreParentGroups = true;
            GetComponent<Image>().raycastTarget = false;

            Slots = new List<Slot>();
            
            if (SetupChildrens)
            {
                SetupSlotsFromChildren();
            }
        }

        private void SetupSlotsFromChildren()
        {
            foreach(SlotGroup SlotGroup in GetComponentsInChildren<SlotGroup>())
            {
                foreach(Slot Slot in SlotGroup.GetComponentsInChildren<Slot>()) Slots.Add(Slot);
            }
        }

        public int GetOrder()
        {
            return Order;
        }

        public bool IsExcluded()
        {
            return Exclude;
        }
    }
}
