using Unity.Entities;

namespace TMG.RollABallDOTS
{
    public struct PlayerMoveSpeed : IComponentData
    {
        public float Value;
    }

    public struct PickupRotateSpeed : IComponentData
    {
        public float Value;
    }
}