using Unity.Burst;
using Unity.Entities;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This system is responsible for destroying any pickups that the player collides with.
    /// It is marked with the [RequireMatchingQueriesForUpdate] attribute so the OnUpdate method only runs during
    /// frames where a pickup entity needs to be destroyed.
    /// </summary>
    [RequireMatchingQueriesForUpdate]
    public partial struct DestroyPickupSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            // We will use one of the built-in entity command buffers to destroy the pickup entity
            // So we just put this line in here to ensure this exists before writing commands to it
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        /// <summary>
        /// OnUpdate methods are typically ran every frame, but due to the [RequireMatchingQueriesForUpdate] attribute
        /// on this ISystem struct, it will only run when the query defined below has matching entities. Meaning this
        /// system will only run when there is an entity that has been picked up that needs to be destroyed.
        /// </summary>
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // This is a built-in entity command buffer that we can write commands to. Unity automatically executes
            // these commands at specified points in the frame.
            var ecbSingleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            // Iterate through all the entities that have a "PickedUpThisFrameTag" on it. We'll just need a reference
            // to the entity so we can destroy it.
            foreach (var (tag, pickupEntity) in SystemAPI.Query<PickedUpThisFrameTag>().WithEntityAccess())
            {
                // This command doesn't immediately destroy the entity. Rather it schedules the deletion of the entity
                // for later and Unity does the actual destruction when the commands of this ECB are played back.
                ecb.DestroyEntity(pickupEntity);
            }
        }
    }
}