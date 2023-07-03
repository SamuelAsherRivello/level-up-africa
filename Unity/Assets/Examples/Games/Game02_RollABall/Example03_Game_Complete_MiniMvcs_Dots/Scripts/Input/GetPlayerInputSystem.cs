using Unity.Entities;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This system constantly updates a PlayerMoveInput component with the Vector2 value that corresponds from the keys
    /// the player is currently pressing
    /// This system runs at the end of the initialization system group so that gameplay systems that run in the later
    /// simulation system group, can read current input data for that frame.
    /// </summary>
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class GetPlayerInputSystem : SystemBase
    {
        // Reference to our custom input asset
        private RollABallInput _rollABallInput;

        protected override void OnCreate()
        {
            // Only run the OnUpdate method for this system if an entity with a PlayerMoveInput component exists in the
            // Entity World.
            RequireForUpdate<PlayerMoveInput>();
            
            // Instantiate and enable our custom input asset
            _rollABallInput = new RollABallInput();
            _rollABallInput.Enable();
        }

        protected override void OnUpdate()
        {
            // Read the current input via our custom input asset
            Vector2 currentPlayerInput = _rollABallInput.Standard.PlayerMoveInput.ReadValue<Vector2>();

            // Write the data to a PlayerMoveInput singleton component
            // A component is considered a "singleton" component if exactly one entity exists in the game world with the
            // specified component
            SystemAPI.SetSingleton(new PlayerMoveInput { Value = currentPlayerInput });
        }
    }
}