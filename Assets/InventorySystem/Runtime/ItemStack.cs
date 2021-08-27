using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class ItemStack
    {
        public Sprite Sprite { get; protected set; }
        public string Name { get; protected set; }
        public bool Stackable { get; }

        public ItemStack(bool Stackable, string Name, Sprite Sprite)
        {
            this.Stackable = Stackable;
            this.Name = Name;
            this.Sprite = Sprite;
        }

    }
}