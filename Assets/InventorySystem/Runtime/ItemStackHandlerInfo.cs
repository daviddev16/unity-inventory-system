namespace InventorySystem.Internals
{
    [System.Serializable]
    public struct ItemStackHandlerInfo    
    {
        public ItemStack ItemStack { get; set; }
        public int Amount { get; set; }

        public ItemStackHandlerInfo(ItemStack ItemStack, int Amount)
        {
            this.ItemStack = ItemStack;
            this.Amount = Amount;
        }
    }
}