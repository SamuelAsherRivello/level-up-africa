using Unity.Entities;

namespace TMG.RollABallDOTS
{
    public struct PickupCounter : IComponentData
    {
        public int CurrentCount;
        public int TargetCount;
    }
}