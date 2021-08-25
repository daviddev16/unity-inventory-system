namespace InventorySystem
{
    public interface InventoryEntityState
    {
        bool ValidationStage();

        void UpdateStage();
    }
}