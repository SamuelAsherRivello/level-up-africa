using Unity.Entities;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    public class PickupAudioSourceAuthoring : MonoBehaviour
    {
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