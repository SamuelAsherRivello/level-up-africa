using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This system schedules a job to detect a trigger event between the player ball and pickups. These trigger systems
    /// need to run in a specific place in the physics simulation to execute properly.
    /// </summary>
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(ExportPhysicsWorld))]
    public partial struct PickupTriggerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            // Be sure to only run this system if certain singletons exist in the world. These are Unity default
            // singletons so we can typically guarantee they will exist.
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var simSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            
            // Schedule the job defined below. Note that doing this doesn't run the job immediately. Rather the job is
            // queued up and executed when a worker thread is available to run it.
            state.Dependency = new PickupTriggerJob
            {
                PlayerLookup = SystemAPI.GetComponentLookup<PlayerTag>(),
                PickupLookup = SystemAPI.GetComponentLookup<PickupTag>(),
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule(simSingleton, state.Dependency);
        }
    }
    
    /// <summary>
    /// This job detects trigger events. We need to do some checking to ensure that the trigger event is between the
    /// player and a pickup.
    /// </summary>
    public struct PickupTriggerJob : ITriggerEventsJob
    {
        // We can use Component Lookups to determine if an arbitrary entity has a specific component
        [ReadOnly] public ComponentLookup<PlayerTag> PlayerLookup;
        [ReadOnly] public ComponentLookup<PickupTag> PickupLookup;

        public EntityCommandBuffer ECB;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            // Get a reference to the two entities involved in the trigger event
            var entityA = triggerEvent.EntityA;
            var entityB = triggerEvent.EntityB;
            
            // Determine which of the two entities is the player entity
            var isEntityAPlayer = PlayerLookup.HasComponent(entityA);
            var isEntityBPlayer = PlayerLookup.HasComponent(entityB);

            // Ensure that one and only one entity is a player entity
            if((!isEntityAPlayer && !isEntityBPlayer) || (isEntityAPlayer && isEntityBPlayer)){return;}

            // The other entity that is not the player is the entity the player collided with
            var hitEntity = isEntityAPlayer ? entityB : entityA;
            
            // Check if the hit entity is a pickup entity. If it is not, return from this job
            if(!PickupLookup.HasComponent(hitEntity)){return;}

            // If the hit entity is a pickup, add the PickedUpThisFrameTag so the pickup systems can run on that entity
            ECB.AddComponent<PickedUpThisFrameTag>(hitEntity);
        }
    }
}