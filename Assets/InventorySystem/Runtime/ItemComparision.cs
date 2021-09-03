namespace InventorySys
{
    public interface ItemComparision
    {
        bool IsSimilar(ItemStack itemStack, int comparisonLevel);
        bool IsSimilar(ItemStackHandler itemHandler, int comparisonLevel);
    }
}