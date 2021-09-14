namespace InventorySys
{
    public class TrackEvent
    {
        public Slot TrackedSlot { get;  }
        public ItemStack ItemStack { get; }
        public bool IsAbsent { get; }

        public TrackEvent(Slot trackedSlot, ItemStack itemStack)
        {
            TrackedSlot = trackedSlot;
            ItemStack = itemStack;
            IsAbsent = ItemStack == null;
        }
    }
}