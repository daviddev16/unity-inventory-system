using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public sealed class ItemStackHandler : MonoBehaviour, ItemComparision
    {
        private ItemStackHandlerInfo ItemInfo;

        public void UpdateHandlerInfo(ItemStack itemStack, int amount)
        {
            ItemInfo = new ItemStackHandlerInfo(itemStack, amount);
            UpdateStates();
        }

        public void SetHandlerInfo(ItemStackHandlerInfo ItemInfo)
        {
            this.ItemInfo = ItemInfo;
            UpdateStates();
        }

        public void ChangeAmount(bool increase, int value)
        {
            if (increase) { ItemInfo.Amount += value; }
            else          { ItemInfo.Amount -= value; }

            UpdateStates();
        }

        public void SetSlotParent(Slot Slot)
            {
                transform.SetParent(Slot.transform);
            }

        public void ResolveTransform()
        {
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public void UpdateStates()
        {
            GetComponentInChildren<UnityEngine.UI.Text>().text = "" + ItemInfo.Amount;
        }

        public bool IsSimilar(ItemStack ItemStack)
        {
            return ItemInfo.ItemStack.Equals(ItemStack);
        }
    }
}