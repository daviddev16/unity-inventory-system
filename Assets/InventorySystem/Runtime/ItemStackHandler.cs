using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public class ItemStackHandler : MonoBehaviour
    {
        public ItemStackHandlerInfo ItemInfo;

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

        public void UpdateStates()
        {
            GetComponentInChildren<UnityEngine.UI.Text>().text = "" + ItemInfo.Amount;
        }

    }
}