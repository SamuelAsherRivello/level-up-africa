using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This system moves the player in 3D space.
    /// </summary>
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(PhysicsSystemGroup))]
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

            foreach (var (velocity, mass, moveForce) in SystemAPI.Query<RefRW<PhysicsVelocity>,PhysicsMass, PlayerMoveForce>().WithAll<PlayerTag>())
            {
                float3 currentMoveInput = new float3(currentPlayerInput.x, 0f, currentPlayerInput.y) * moveForce.Value * deltaTime;
                velocity.ValueRW.ApplyLinearImpulse(in mass, currentMoveInput);
            }
        }
    }
}