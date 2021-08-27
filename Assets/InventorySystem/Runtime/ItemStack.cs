using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "ItemStack", menuName = "New ItemStack", order = -1)]
    public class ItemStack : ScriptableObject
    {
        public Sprite Sprite;
        public string Name;
        public int ID;
        public bool Stackable;

        public ItemStack(int ID, bool Stackable, string Name, Sprite Sprite)
        {
            this.Stackable = Stackable;
            this.Name = Name;
            this.ID = ID;
            this.Sprite = Sprite;
        }

    }
}