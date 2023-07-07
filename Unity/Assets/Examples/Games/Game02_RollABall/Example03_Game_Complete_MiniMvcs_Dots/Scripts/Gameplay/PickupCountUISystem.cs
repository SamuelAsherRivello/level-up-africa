using System;
using Unity.Entities;

namespace TMG.RollABallDOTS
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial class PickupCountUISystem : SystemBase
    {
        public Action<PickupCounter> OnIncrementPickupCount;
        
        protected override void OnUpdate()
        {
            foreach (var pickupCounter in SystemAPI.Query<PickupCounter>().WithChangeFilter<PickupCounter>())
            {
                OnIncrementPickupCount?.Invoke(pickupCounter);
            }
        }
    }
}