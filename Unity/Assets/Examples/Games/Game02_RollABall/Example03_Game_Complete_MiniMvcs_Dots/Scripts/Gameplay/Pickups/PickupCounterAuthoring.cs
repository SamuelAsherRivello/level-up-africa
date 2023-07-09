using Unity.Entities;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This authoring component allows us to define the target number of pickups to trigger the game over state.
    /// </summary>
    public class PickupCounterAuthoring : MonoBehaviour
    {
        public int TargetCount;

        /// <summary>
        /// The baker class defines what happens when we convert the GameObject the authoring component has into an
        /// entity. In this case we just set the initial values for the PickupCounter data component.
        /// </summary>
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