using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public class Slot : MonoBehaviour
    {

        [SerializeField]
        public ItemStackHandler ItemStackHandler;

        public void Populate(ItemStack ItemStack, int amount)
        {
            GameObject newItemHandler = Instantiate(ISA.GetItemStackHandlerAsset(), transform.position, Quaternion.identity);
            newItemHandler.transform.SetParent(transform);
            newItemHandler.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            ItemStackHandler = newItemHandler.GetComponent<ItemStackHandler>();
            ItemStackHandler.UpdateHandlerInfo(ItemStack, amount);
        }

        public void RenameWithIndex(ref int SlotIndex)
        {
            gameObject.name = "Slot " + SlotIndex;
        }

        public bool IsEmpty()
        {
            return ItemStackHandler == null;
        }

    }
}