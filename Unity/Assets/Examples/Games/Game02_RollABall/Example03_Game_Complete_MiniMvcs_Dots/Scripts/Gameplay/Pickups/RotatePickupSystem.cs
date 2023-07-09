using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.RollABallDOTS
{
    public partial struct RotatePickupSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            new RotatePickupJob { DeltaTime = deltaTime }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct RotatePickupJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(ref LocalTransform transform, in PickupRotation pickupRotation)
        {
            transform = transform.Rotate(quaternion.Euler(pickupRotation.Vector * pickupRotation.Speed * DeltaTime));
        }
    }
}