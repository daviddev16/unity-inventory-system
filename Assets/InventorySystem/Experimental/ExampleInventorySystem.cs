namespace InventorySys.Experimentals
{
    public class ExampleInventorySystem : InventorySystem
    {
        void Start()
        {
            AddAllSamples();
            Set("Milk", 20, 20);

            Inventory.GetInventory().GetSlot(10).AddTracker(new DebugSlotTracker());

        }
    }
}