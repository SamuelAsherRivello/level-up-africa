using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace TMG.RollABallDOTS
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(ExportPhysicsWorld))]
    public partial struct PickupTriggerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var simSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            
            state.Dependency = new PickupTriggerJob
            {
                PlayerLookup = SystemAPI.GetComponentLookup<PlayerTag>(),
                PickupLookup = SystemAPI.GetComponentLookup<PickupTag>(),
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule(simSingleton, state.Dependency);
        }
    }
    
    public struct PickupTriggerJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<PlayerTag> PlayerLookup;
        [ReadOnly] public ComponentLookup<PickupTag> PickupLookup;

        public EntityCommandBuffer ECB;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            var entityA = triggerEvent.EntityA;
            var entityB = triggerEvent.EntityB;
            
            var isEntityAPlayer = PlayerLookup.HasComponent(entityA);
            var isEntityBPlayer = PlayerLookup.HasComponent(entityB);

            if((!isEntityAPlayer && !isEntityBPlayer) || (isEntityAPlayer && isEntityBPlayer)){return;}

            var playerEntity = isEntityAPlayer ? entityA : entityB;
            var hitEntity = isEntityAPlayer ? entityB : entityA;
            
            if(!PickupLookup.HasComponent(hitEntity)){return;}

            ECB.AddComponent<PickedUpThisFrameTag>(hitEntity);
        }
    }
}