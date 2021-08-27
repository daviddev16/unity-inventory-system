namespace InventorySystem
{
    [System.Serializable]
    public class ItemStack
    {
        public bool Stackable { get; }

        public ItemStack(bool Stackable)
        {
            this.Stackable = Stackable;
        }

    }
}