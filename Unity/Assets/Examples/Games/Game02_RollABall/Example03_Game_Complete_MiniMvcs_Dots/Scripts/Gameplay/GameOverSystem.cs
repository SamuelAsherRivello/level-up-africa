using Unity.Entities;

namespace TMG.RollABallDOTS
{
    public partial class GameOverSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<GameOverTag>();
        }

        protected override void OnStartRunning()
        {
            var simulationSystemGroup = World.GetExistingSystemManaged(typeof(SimulationSystemGroup));
            simulationSystemGroup.Enabled = false;
        }

        protected override void OnUpdate()
        {
            
        }
    }
}