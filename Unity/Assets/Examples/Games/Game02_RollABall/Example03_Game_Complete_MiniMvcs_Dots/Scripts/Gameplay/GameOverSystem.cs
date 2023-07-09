using System;
using Unity.Entities;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This system runs when an entity with the GameOverTag exists in the entity world. When this happens, this system
    /// invokes an event that our UI controller listens to, to change some text on the screen.
    /// </summary>
    public partial class GameOverSystem : SystemBase
    {
        public Action OnGameOver;
        
        protected override void OnCreate()
        {
            // Only run this system if an entity with this tag exists
            RequireForUpdate<GameOverTag>();
        }

        // OnStartRunning is called before the first iteration of the OnUpdate method. It will only be called if an
        // entity with the GameOverTag exists in the entity world.
        protected override void OnStartRunning()
        {
            OnGameOver?.Invoke();
         
            // This disables the simulation system group, which effectively pauses the game.
            var simulationSystemGroup = World.GetExistingSystemManaged(typeof(SimulationSystemGroup));
            simulationSystemGroup.Enabled = false;
        }

        protected override void OnUpdate()
        {
            
        }
    }
}