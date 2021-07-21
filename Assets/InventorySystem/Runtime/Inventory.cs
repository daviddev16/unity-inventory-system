using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Inventory : MonoBehaviour, AbstractInventory
{
    public List<DefaultContainer> Containers { get => containers; set => containers = value; }

    [SerializeField] private List<DefaultContainer> containers;

    void Start()
    {
        foreach (DefaultContainer childContainer in GetComponentsInChildren<DefaultContainer>())
        {
            containers.Add(childContainer);
        }

        containers.Sort();

        int CurrentSlot = 0;
        foreach(DefaultContainer container in Containers)
        {
            foreach(Slot childSlot in container.GetComponentsInChildren<Slot>())
            {
                childSlot.name = "Slot " + CurrentSlot;
                container.Slots.Add(childSlot);
                CurrentSlot++;
            }
        }

        for(int i = 0; i < 22; i++)
        {
            AddItem(new AbstractItem(false, "Diamond Sword"));
        }
    }

    public void AddItem(AbstractItem Item)
    {
        EachSlot(Slot => 
        {

            if (Slot.IsEmpty())
            {

                DisplayedItem displayedItem = Instantiate(InventorySystemSetup.DisplayedItemPrefab, 
                    Vector3.zero, Quaternion.identity, Slot.transform).GetComponent<DisplayedItem>();

                displayedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                displayedItem.Item = Item;
                Slot.DisplayedItem = displayedItem;


                return true;
            }

            return false;
        });
    }

    public void RemoveItem(AbstractItem Item)
    {
    }
    public bool ContainsItem(AbstractItem item)
    {
        return false;
    }

    private void EachSlot(System.Predicate<Slot> SlotPredicate)
    {
        bool End = false;
        foreach(DefaultContainer container in Containers)
        {
            if (!End)
            {
                foreach (Slot slot in container.Slots)
                {
                    if (SlotPredicate.Invoke(slot))
                    {
                        End = true;
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }
    }
}

