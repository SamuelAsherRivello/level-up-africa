using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This system moves the player in 3D space. Currently it is directly modifying the transform, but I may change
    /// this to push the entity by a physics force for the demo.
    /// </summary>
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct MovePlayerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerMoveInput>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float2 currentPlayerInput = SystemAPI.GetSingleton<PlayerMoveInput>().Value;
            float deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (transform, moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, PlayerMoveSpeed>().WithAll<PlayerTag>())
            {
                float3 currentMoveInput = new float3(currentPlayerInput.x, 0f, currentPlayerInput.y) * moveSpeed.Value;
                transform.ValueRW.Position += currentMoveInput * deltaTime;
            }
        }
    }
}