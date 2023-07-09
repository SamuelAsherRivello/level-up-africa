using Unity.Burst;
using Unity.Entities;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This system is responsible for incrementing the internal pickup counter data. If it is greater than the target
    /// number, it will also begin to trigger the game over state.
    /// As this system has the [RequireMatchingQueriesForUpdate] attribute, it will only run on the frames when a
    /// pickup has been picked up and the counter needs to increment.
    /// </summary>
    [RequireMatchingQueriesForUpdate]
    public partial struct IncrementPickupCountSystem : ISystem
    {
        // This query is for all the pickup entities that have been picked up this frame
        private EntityQuery _pickupQuery;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PickupCounter>();
            _pickupQuery = SystemAPI.QueryBuilder().WithAll<PickedUpThisFrameTag, PickupTag>().Build();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // Get the number of entities we picked up this frame.
            var pickupsThisFrame = _pickupQuery.CalculateEntityCount();

            if(pickupsThisFrame <= 0) return;
            
            // Increment the internal pickup counter
            var pickupCounter = SystemAPI.GetSingleton<PickupCounter>();
            pickupCounter.CurrentCount += pickupsThisFrame;
            SystemAPI.SetSingleton(pickupCounter);

            // If we have reached or surpassed the target pickup count, begin the game over state.
            if (pickupCounter.CurrentCount >= pickupCounter.TargetCount)
            {
                var gameControllerEntity = SystemAPI.GetSingletonEntity<PickupCounter>();
                state.EntityManager.AddComponent<GameOverTag>(gameControllerEntity);
            }
        }
    }
}