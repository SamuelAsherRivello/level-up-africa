using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    public class PickupRotationAuthoring : MonoBehaviour
    {
        public float3 Vector;
        public float Speed;

        public class PickupRotationBaker : Baker<PickupRotationAuthoring>
        {
            public override void Bake(PickupRotationAuthoring authoring)
            {
                Entity pickupEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PickupTag>(pickupEntity);
                AddComponent(pickupEntity, new PickupRotation
                {
                    Vector = new float3
                    {
                        x = math.radians(authoring.Vector.x),
                        y = math.radians(authoring.Vector.y),
                        z = math.radians(authoring.Vector.z)
                    }, 
                    Speed = authoring.Speed
                });
            }
        }
    }
}