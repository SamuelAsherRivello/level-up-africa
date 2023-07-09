using Unity.Entities;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// Data component that keeps track of the number of pickups the player has picked up, and the target number of
    /// pickups to trigger the game over state.
    /// </summary>
    public struct PickupCounter : IComponentData
    {
        public int CurrentCount;
        public int TargetCount;
    }
}