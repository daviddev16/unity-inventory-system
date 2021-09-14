
namespace InventorySys.Experimentals
{
    public class DebugSlotTracker : ASlotTracker
    {
        public override void OnTrackStateEvent(TrackEvent trackEvent)
        {
            if(trackEvent.IsAbsent)
            {
                UnityEngine.Debug.Log(trackEvent.TrackedSlot.name + " : Empty.");
            }
            else
            {
                UnityEngine.Debug.Log(trackEvent.TrackedSlot.name + " : " + trackEvent.ItemStack.Name);
            }
        }
    }
}