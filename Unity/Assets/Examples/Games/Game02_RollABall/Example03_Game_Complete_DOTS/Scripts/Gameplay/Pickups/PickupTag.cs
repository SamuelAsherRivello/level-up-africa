using Unity.Entities;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This component allows us to get all entities that are pickups so we can perform operations on all of them.
    /// </summary>
    public struct PickupTag : IComponentData {}
    
    /// <summary>
    /// This component gets added to a pickup if it has been picked up this frame. An entity with this component will
    /// then run on systems to increment the score counter, play a sound, and destroy the pickup.
    /// </summary>
    public struct PickedUpThisFrameTag : IComponentData {}
}