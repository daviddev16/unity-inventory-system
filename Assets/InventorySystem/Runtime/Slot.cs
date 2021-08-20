using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public class Slot : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RenameWithIndex(ref int SlotIndex)
        {
            gameObject.name = "Slot " + SlotIndex;
        }

    }


}