using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
    public sealed class ItemStackHandler : MonoBehaviour, ItemComparision, 
        IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private ItemStackHandlerInfo ItemInfo;


        public void UpdateStates()
        {
            if (ValidateState())
            {
                GetComponentInChildren<Text>().text = "" + ItemInfo.Amount;
            }
        }

        private bool ValidateState()
        {
            if (ItemInfo.Amount <= 0)
            {
                SelfPurge();
                return false;
            }

            return true;
        }

        public void UpdateHandlerInfo(ItemStack itemStack, int amount)
        {
            ItemInfo = new ItemStackHandlerInfo(itemStack, amount);
            UpdateStates();
        }


        public void ChangeAmount(bool increase, int value)
        {
            if (increase) { ItemInfo.Amount += value; }
            else { ItemInfo.Amount -= value; }

            UpdateStates();
        }

        public bool IsFree()
        {
            return ItemInfo.Amount < GetItemStack().StackLimit;
        }

        public void SetHandlerInfo(ItemStackHandlerInfo ItemInfo)
        {
            this.ItemInfo = ItemInfo;
            UpdateStates();
        }

        public void ResolveTransform()
        {
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public bool IsSimilar(ItemStack ItemStack)
        {
            return ItemInfo.ItemStack.Equals(ItemStack);
        }

        public void SetSlotParent(Slot Slot)
        {
            transform.SetParent(Slot.transform);
        }
        
        public ItemStack GetItemStack()
        {
            return ItemInfo.ItemStack;
        }

        public void SelfPurge()
        {
            Destroy(gameObject);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GameObject CurrentRaycast = eventData.pointerCurrentRaycast.gameObject; 
            if(CurrentRaycast != null)
            {
                if (CurrentRaycast.GetComponent<Slot>())
                {
                    CurrentRaycast.GetComponent<Slot>().Migrate(this);
                }
            }


        }
    }
}