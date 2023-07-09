using Unity.Burst;
using Unity.Entities;

namespace TMG.RollABallDOTS
{
    [RequireMatchingQueriesForUpdate]
    public partial struct IncrementPickupCountSystem : ISystem
    {
        private EntityQuery _pickupQuery;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PickupCounter>();
            _pickupQuery = SystemAPI.QueryBuilder().WithAll<PickedUpThisFrameTag, PickupTag>().Build();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var pickupsThisFrame = _pickupQuery.CalculateEntityCount();

            if(pickupsThisFrame <= 0) return;
            
            var pickupCounter = SystemAPI.GetSingleton<PickupCounter>();
            pickupCounter.CurrentCount += pickupsThisFrame;
            SystemAPI.SetSingleton(pickupCounter);

            if (pickupCounter.CurrentCount >= pickupCounter.TargetCount)
            {
                var gameControllerEntity = SystemAPI.GetSingletonEntity<PickupCounter>();
                state.EntityManager.AddComponent<GameOverTag>(gameControllerEntity);
            }
        }
    }
}