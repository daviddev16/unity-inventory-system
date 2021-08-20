using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InventorySystem
{
    public class Container : MonoBehaviour, IComparer<Container>
    {

        public Inventory Owner { protected set; get; }

        [SerializeField] public List<Slot> Slots { protected set; get; }
        [SerializeField] public int Order { protected set; get; } = 0;

        void Start()
        {

        }

        void Update()
        {

        }

        public int Compare(Container x, Container y)
        {
            if (x.Order > y.Order) return -1;
            else if (x.Order < y.Order) return 1;
            return 0;
        }
    }
}
