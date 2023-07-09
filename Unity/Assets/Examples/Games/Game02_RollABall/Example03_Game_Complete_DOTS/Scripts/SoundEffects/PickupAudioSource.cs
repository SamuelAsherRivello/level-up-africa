using Unity.Entities;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This is a special component as it is a managed cleanup component. This is a more intermediate concept of Unity
    /// ECS, but it allows us to run initialization and cleanup logic. Also note that it is a class, not a struct as
    /// data components typically are. This allows us to use managed types that we wouldn't be able to otherwise. Note
    /// that this does come with some restrictions as we cannot use the burst compiler or job system with them.
    /// </summary>
    public class PickupAudioSource : ICleanupComponentData
    {
        public AudioSource Value;
    }

    /// <summary>
    /// This tag is used in conjunction with the PickupAudioSource cleanup component to run the initialization and
    /// cleanup logic.
    /// </summary>
    public struct PickupAudioSourceTag : IComponentData{}
}