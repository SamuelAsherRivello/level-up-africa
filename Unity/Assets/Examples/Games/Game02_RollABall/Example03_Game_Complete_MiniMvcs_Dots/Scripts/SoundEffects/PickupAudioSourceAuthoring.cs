using Unity.Entities;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This authoring component is added to an entity in the RollABallSubScene.
    /// </summary>
    public class PickupAudioSourceAuthoring : MonoBehaviour
    {
        /// <summary>
        /// This Baker adds a PickupAudioSourceTag to the outputted entity which will trigger the initialization logic
        /// of the PickupAudioSource cleanup component.
        /// </summary>
        public class PickupAudioSourceBaker : Baker<PickupAudioSourceAuthoring>
        {
            public override void Bake(PickupAudioSourceAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PickupAudioSourceTag>(entity);
            }
        }
    }
}