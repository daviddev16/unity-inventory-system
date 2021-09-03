namespace InventorySys.Internals
{
    public interface IReceivable
    {
        bool CanReceive(ItemStack itemStack);
    }
}