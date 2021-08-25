using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
    public sealed class ItemStackHandler : MonoBehaviour, ItemComparision, InventoryEntityState, 
        IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        private ItemStackHandlerInfo ItemInfo;

        public void UpdateHandlerInfo(ItemStack itemStack, int amount)
        {
            ItemInfo = new ItemStackHandlerInfo(itemStack, amount);
            UpdateStage();
        }
        
        public void ChangeAmount(bool increase, int value)
        {
            if (increase) { ItemInfo.Amount += value; }
            else { ItemInfo.Amount -= value; }
            UpdateStage();
        }

        public bool IsFree()
        {
            return ItemInfo.Amount < GetItemStack().StackLimit;
        }

        public void SetHandlerInfo(ItemStackHandlerInfo ItemInfo)
        {
            this.ItemInfo = ItemInfo;
            UpdateStage();
        }

        public bool ValidationStage()
        {
            if (!Exists())
            {
                SelfPurge();
            }
            return true;
        }

        public void UpdateStage()
        {
            if (ValidationStage())
            {
                GetComponentInChildren<Text>().text = "" + ItemInfo.Amount;
            }
        }

        public void SetupToSlot(Slot Slot)
        {
            SetSlotParent(Slot);
            ResolveTransform();
            Slot.UpdateStage();
        }

        public void ResolveTransform()
        {
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GameObject CurrentRaycast = eventData.pointerCurrentRaycast.gameObject;

            if (CurrentRaycast != null)
            {
                if (CurrentRaycast.GetComponent<Slot>())
                {
                    CurrentRaycast.GetComponent<Slot>().Migrate(this);
                }
                else if (CurrentRaycast.GetComponent<ItemStackHandler>())
                {
                    Slot ParentSlot = CurrentRaycast.GetComponentInParent<Slot>();
                    Slot CurrentSlot = GetComponentInParent<Slot>();

                    CurrentSlot.SwitchItemsBySlots(ParentSlot);
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public bool IsSimilar(ItemStack ItemStack)
        {
            return ItemInfo.ItemStack.Equals(ItemStack);
        }

        public bool IsSimilar(ItemStackHandler ItemStackHandler)
        {
            return IsSimilar(ItemStackHandler.GetItemStack());
        }

        public void SetSlotParent(Slot Slot)
        {
            transform.SetParent(Slot.transform);
        }
        
        public ItemStackHandlerInfo CopyInformation()
        {
            return (ItemStackHandlerInfo) ItemInfo.Clone();
        }

        public ItemStack GetItemStack()
        {
            return ItemInfo.ItemStack;
        }

        private void SelfPurge()
        {
            Destroy(gameObject);
        }

        private bool Exists()
        {
            if (ItemInfo.Amount <= 0)
            {
                return false;
            }

            return true;
        }

    }
}