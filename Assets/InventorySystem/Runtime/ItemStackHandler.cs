using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
    public sealed class ItemStackHandler : MonoBehaviour, ItemComparision, InventoryEntityState, 
        IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        private ItemStackHandlerInfo ItemInfo;
        public Slot ParentSlot;

        private Vector2 beginPos;

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
            return (GetItemStack().Stackable);
        }

        public void SetHandlerInfo(ItemStackHandlerInfo ItemInfo)
        {
            this.ItemInfo = ItemInfo;
            UpdateStage();
        }

        public void UpdateStage()
        {
            if (!CheckAmount())
            {
                SelfPurge();
            }

            ParentSlot = GetComponentInParent<Slot>();
            GetComponentInChildren<Text>().text = "" + ItemInfo.Amount;
        }

        public void ResolveTransform()
        {
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            beginPos = eventData.position;
            transform.SetParent(GetComponentInParent<Inventory>().transform);
            transform.SetAsFirstSibling();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            InventoryEntityState EntityState = eventData.pointerCurrentRaycast.gameObject.GetComponent<InventoryEntityState>();

            if (EntityState != null)
            {
                if (EntityState is Slot)
                {
                    (EntityState as Slot).Migrate(this);
                }
                else if (EntityState is ItemStackHandler)
                {
                    Slot OtherItemSlot = (EntityState as ItemStackHandler).ParentSlot;
                    ParentSlot.SwitchItemsBySlots(OtherItemSlot);
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = new Vector3(eventData.position.x, eventData.position.y, 0);
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

        public void SelfPurge()
        {
            Destroy(gameObject);
        }

        private bool CheckAmount()
        {
            if (ItemInfo.Amount <= 0)
            {
                return false;
            }

            return true;
        }

    }
}