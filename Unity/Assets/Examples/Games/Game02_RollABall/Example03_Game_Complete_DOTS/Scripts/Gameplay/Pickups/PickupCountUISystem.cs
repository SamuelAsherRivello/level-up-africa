using System;
using Unity.Entities;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This system is responsible for invoking an event to update the pickup counter UI
    /// </summary>
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial class PickupCountUISystem : SystemBase
    {
        // This is the event that will be invoked. It passes along the current PickupCounter values to inform the UI
        // on how many pickups the user has picked up and the target number of pickups
        public Action<PickupCounter> OnIncrementPickupCount;
        
        protected override void OnUpdate()
        {
            // Iterate through all entities with the PickupCounter component. Although only one entity with this type
            // will exist in our world, we still treat it as a group of entities.
            // The .WithChangeFilter<PickupCounter>() option means that this query is only valid when the PickupCounter
            // component has been changed. This means the UI event is only invoked when the counter has been incremented
            foreach (var pickupCounter in SystemAPI.Query<PickupCounter>().WithChangeFilter<PickupCounter>())
            {
                OnIncrementPickupCount?.Invoke(pickupCounter);
            }
        }
    }
}