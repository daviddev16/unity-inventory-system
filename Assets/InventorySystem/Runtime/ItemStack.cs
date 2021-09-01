using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class ItemStack
    {
        public static readonly int LOW_LEVEL_COMPARISON = 0;
        public static readonly int HIGH_LEVEL_COMPARISON = 1;

        public int ID { get; private set; }
        public string Name { get; private set; }
        public byte Data { get; private set; }
        public bool Stackable { get; private set; }
        public UnityEngine.Sprite Sprite { get; private set; }

        public ItemStack(int ID, bool Stackable, string Name, Sprite Sprite)
        {
            this.Stackable = Stackable;
            this.Name = Name;
            this.ID = ID;
            this.Sprite = Sprite;
        }

    }
}