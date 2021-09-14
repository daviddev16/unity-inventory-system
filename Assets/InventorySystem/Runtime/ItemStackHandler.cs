﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySys
{
    using Internals;
    public sealed class ItemStackHandler : MonoBehaviour, ItemComparision, InventoryEntityState,
        IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        private ItemStackHandlerInfo ItemInfo;
        private Slot ParentSlot;

        public void OnBeginDrag(PointerEventData eventData)
        {
            GetComponent<Image>().raycastTarget = false;
            transform.SetParent(GetComponentInParent<Inventory>().transform);
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = new Vector3(eventData.position.x, eventData.position.y, 0);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GetComponent<Image>().raycastTarget = true;
            GameObject raycastTargetGameObject = eventData.pointerCurrentRaycast.gameObject;
            if (raycastTargetGameObject != null)
            {
                InventoryEntityState entityState = raycastTargetGameObject.GetComponent<InventoryEntityState>();
                if (entityState != null && Move(ref entityState))
                {
                    return;
                }
            }
            /* back to the origin */
            transform.SetParent(ParentSlot.transform);
            ResolveTransform();
        }

        private bool Move(ref InventoryEntityState entityState)
        {
            if (entityState is Slot && (entityState as Slot).IsEmpty())
            {
                if ((entityState as Slot).CanReceive(GetItemStack()))
                {
                    MigrateToSlot(entityState as Slot);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (entityState is ItemStackHandler)
            {
                if((entityState as ItemStackHandler).ParentSlot.CanReceive(GetItemStack()) &&
                    ParentSlot.CanReceive((entityState as ItemStackHandler).GetItemStack()))
                {
                    ChangeWithItemHandler(entityState as ItemStackHandler);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private void ChangeWithItemHandler(ItemStackHandler ItemHandler)
        {
            Slot PreviousSlot = ParentSlot;

            transform.SetParent(ItemHandler.ParentSlot.transform);
            ItemHandler.transform.SetParent(PreviousSlot.transform);

            ResolveTransform();
            ItemHandler.ResolveTransform();

            ItemHandler.UpdateEntity();
            PreviousSlot.UpdateEntity();
            UpdateEntity();
            ParentSlot.UpdateEntity();
        }

        private void MigrateToSlot(Slot slot)
        {
            Slot PreviousSlot = ParentSlot;

            transform.SetParent(slot.transform);
            ResolveTransform();

            UpdateEntity();
            PreviousSlot.UpdateEntity();
            slot.UpdateEntity();
        }

        public void ChangeAmount(bool increase, int value)
        {
            if (increase) { ItemInfo.Amount += value; }
            else { ItemInfo.Amount -= value; }
            UpdateEntity();
        }

        public void UpdateEntity()
        {
            if (ItemInfo.Amount <= 0)
            {
                SelfPurge();
            }

            ParentSlot = GetComponentInParent<Slot>();

            GetComponentInChildren<Text>().text = "" + ItemInfo.Amount;
            GetComponent<Image>().sprite = GetItemStack().Sprite;
            gameObject.name = GetItemStack().Name;
        }

        public bool IsSimilar(ItemStack itemStack, int comparisonLevel)
        {
            if(comparisonLevel == ItemStack.HIGH_LEVEL_COMPARISON)
            {
                return (itemStack.ID == GetItemStack().ID) && (itemStack.Data == GetItemStack().Data);
            }
            else if(comparisonLevel == ItemStack.LOW_LEVEL_COMPARISON)
            {
                return (itemStack.ID == GetItemStack().ID);
            }

            return false;
        }

        public bool IsSimilar(ItemStackHandler itemHandler, int comparisonLevel)
        {
            return IsSimilar(itemHandler.GetItemStack(), comparisonLevel);
        }

        public void ResolveTransform()
        {
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public void SetInformation(ItemStackHandlerInfo itemInfo)
        {
            this.ItemInfo = itemInfo;
        }

        public ItemStack GetItemStack()
        {
            return ItemInfo.ItemStack;
        }

        public void SelfPurge()
        {
            Destroy(gameObject);
        }
    }
}