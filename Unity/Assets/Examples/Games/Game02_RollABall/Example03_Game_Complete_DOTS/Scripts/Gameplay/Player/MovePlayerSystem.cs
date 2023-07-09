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
            // First get the current input value from the PlayerMoveInput component. This component is set in the
            // GetPlayerInputSystem that runs earlier in the frame.
            float2 currentPlayerInput = SystemAPI.GetSingleton<PlayerMoveInput>().Value;
            float deltaTime = SystemAPI.Time.DeltaTime;

            // Although there is only one player in the game world, we still define systems as if we were executing over
            // a group of players. Inside this idiomatic foreach, we apply force to the player based off the current
            // move input and the force strength.
            foreach (var (velocity, mass, moveForce) in 
                     SystemAPI.Query<RefRW<PhysicsVelocity>,PhysicsMass, PlayerMoveForce>().WithAll<PlayerTag>())
            {
                float3 moveInput3d = new float3(currentPlayerInput.x, 0f, currentPlayerInput.y);
                float3 currentMoveInput = moveInput3d * moveForce.Value * deltaTime;
                velocity.ValueRW.ApplyLinearImpulse(in mass, currentMoveInput);
            }
        }
    }
}