using Unity.Burst;
using Unity.Entities;

namespace TMG.RollABallDOTS
{
    public partial struct DestroyPickupSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (tag, pickupEntity) in SystemAPI.Query<PickedUpThisFrameTag>().WithEntityAccess())
            {
                ecb.DestroyEntity(pickupEntity);
            }
        }
    }
}