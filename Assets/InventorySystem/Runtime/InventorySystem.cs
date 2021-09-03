using System.Collections;
using UnityEngine;


namespace InventorySys
{
    using Internals;
    public partial class InventorySystem : MonoBehaviour
    {
        [SerializeField] private InventoryBook InventoryBook;

        private void Start()
        {
            if (Inventory.GetInventory() == null)
            {
                Debug.LogWarning("There is no inventory instance.");
                Destroy(gameObject);
            }
        }

        public ItemStack FindItemStack(int id, byte data = 0)
        {
            foreach (ItemStack itemStack in InventoryBook.Items)
            {
                if (itemStack.ID == id && itemStack.Data == data)
                {
                    return itemStack;
                }
            }
            return null;
        }

        public ItemStack FindItemStack(string name)
        {
            foreach (ItemStack itemStack in InventoryBook.Items)
            {
                if (itemStack.Name.Equals(name))
                {
                    return itemStack;
                }
            }
            return null;
        }

        public void Add(int id, byte data = 0)
        {
            ItemStack itemStack = FindItemStack(id, data);
            if (itemStack != null)
            {
                Inventory.GetInventory().AddItemStack(itemStack);
            }
        }

        public void Add(string name)
        {
            ItemStack itemStack = FindItemStack(name);
            if (itemStack != null)
            {
                Inventory.GetInventory().AddItemStack(itemStack);
            }
        }

        public void Remove(int id, byte data = 0)
        {
            ItemStack itemStack = FindItemStack(id, data);
            if (itemStack != null)
            {
                Inventory.GetInventory().RemoveItemStack(itemStack);
            }
        }

        public void Remove(string name)
        {
            ItemStack itemStack = FindItemStack(name);
            if (itemStack != null)
            {
                Inventory.GetInventory().RemoveItemStack(itemStack);
            }
        }

        public bool Contains(int id, byte data = 0)
        {
            ItemStack itemStack = FindItemStack(id, data);
            if (itemStack != null)
            {
                return Inventory.GetInventory().ContainsItem(itemStack);
            }
            return false;
        }
       
        public bool Contains(string name)
        {
            ItemStack itemStack = FindItemStack(name);
            if (itemStack != null)
            {
                return Inventory.GetInventory().ContainsItem(itemStack);
            }
            return false;
        }

        public void Set(int id, int amount, int slot, byte data = 0)
        {
            ItemStack itemStack = FindItemStack(id, data);
            if (itemStack != null)
            {
                ItemStackHandlerInfo information = new ItemStackHandlerInfo(itemStack, amount);
                Inventory.GetInventory().Set(information, Inventory.GetInventory().GetSlot(slot));
            }
        }

        public void Set(string name, int amount, int slot)
        {
            ItemStack itemStack = FindItemStack(name);
            if (itemStack != null)
            {
                ItemStackHandlerInfo information = new ItemStackHandlerInfo(itemStack, amount);
                Inventory.GetInventory().Set(information, Inventory.GetInventory().GetSlot(slot));
            }
        }

        public void AddAllSamples()
        {
            foreach(ItemStack itemStack in InventoryBook.Items)
            {
                Add(itemStack.Name);
            }
        }

    }
}