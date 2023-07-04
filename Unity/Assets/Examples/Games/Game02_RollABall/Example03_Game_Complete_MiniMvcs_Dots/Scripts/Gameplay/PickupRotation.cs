using Unity.Entities;
using Unity.Mathematics;

namespace TMG.RollABallDOTS
{
    public struct PickupRotation : IComponentData
    {
        public float3 Vector;
        public float Speed;
    }
}