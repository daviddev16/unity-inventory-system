using UnityEngine;

namespace InventorySys
{
    [System.Serializable]
    public class ItemStack
    {
        public static readonly int LOW_LEVEL_COMPARISON = 0;
        public static readonly int HIGH_LEVEL_COMPARISON = 1;

        public int ID;
        public string Name;
        public byte Data;
        public bool Stackable;
        public UnityEngine.Sprite Sprite;

        public ItemStack(int ID, bool Stackable, string Name, Sprite Sprite)
        {
            this.Stackable = Stackable;
            this.Name = Name;
            this.ID = ID;
            this.Sprite = Sprite;
        }

    }
}