using Unity.Entities;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This tag is added to an entity to signal that the player has collected a sufficient number of pickups to win
    /// </summary>
    public struct GameOverTag : IComponentData {}
}