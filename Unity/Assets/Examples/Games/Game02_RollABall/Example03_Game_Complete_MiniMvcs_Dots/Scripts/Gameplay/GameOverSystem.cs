using System;
using Unity.Entities;

namespace TMG.RollABallDOTS
{
    public partial class GameOverSystem : SystemBase
    {
        public Action OnGameOver;
        
        protected override void OnCreate()
        {
            RequireForUpdate<GameOverTag>();
        }

        protected override void OnStartRunning()
        {
            OnGameOver?.Invoke();
            
            var simulationSystemGroup = World.GetExistingSystemManaged(typeof(SimulationSystemGroup));
            simulationSystemGroup.Enabled = false;
        }

        protected override void OnUpdate()
        {
            
        }
    }
}