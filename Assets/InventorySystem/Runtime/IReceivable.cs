namespace InventorySystem.Internals
{
    public interface IReceivable
    {
        bool CanReceive(ItemStack itemStack);
    }
}