namespace InventorySys
{
    public abstract class ASlotTracker
    {
        public abstract void OnTrackStateEvent(TrackEvent trackEvent);
    }
}