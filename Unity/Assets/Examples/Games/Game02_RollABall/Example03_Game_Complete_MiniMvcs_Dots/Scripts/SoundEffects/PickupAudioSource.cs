using Unity.Entities;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    public class PickupAudioSource : ICleanupComponentData
    {
        public AudioSource Value;
    }

    public struct PickupAudioSourceTag : IComponentData{}
}