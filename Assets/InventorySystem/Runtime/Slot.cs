using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public class Slot : MonoBehaviour
    {
        public void RenameWithIndex(ref int SlotIndex)
        {
            gameObject.name = "Slot " + SlotIndex;
        }

    }
}