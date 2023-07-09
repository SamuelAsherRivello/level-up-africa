using Unity.Entities;
using Unity.Mathematics;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This data component defines the speed and direction the pickups will rotate in the game world.
    /// </summary>
    public struct PickupRotation : IComponentData
    {
        public float Speed;
        public float3 Direction;
    }
}