using Unity.Entities;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    public class PickupCounterAuthoring : MonoBehaviour
    {
        public int TargetCount;

        public class PickupCounterBaker : Baker<PickupCounterAuthoring>
        {
            public override void Bake(PickupCounterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new PickupCounter
                    {
                        CurrentCount = 0, 
                        TargetCount = authoring.TargetCount
                    });
            }
        }
    }
}