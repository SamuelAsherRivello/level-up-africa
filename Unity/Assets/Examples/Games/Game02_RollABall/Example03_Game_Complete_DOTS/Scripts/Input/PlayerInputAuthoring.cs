using Unity.Entities;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This authoring component is added to an entity in the RollABallSubScene.
    /// </summary>
    public class PlayerInputAuthoring : MonoBehaviour
    {
        /// <summary>
        /// This Baker will generate a new entity and add a PlayerMoveInput component that we can write to and reference
        /// from multiple systems
        /// </summary>
        public class PlayerInputAuthoringBaker : Baker<PlayerInputAuthoring>
        {
            public override void Bake(PlayerInputAuthoring authoring)
            {
                Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PlayerMoveInput>(inputEntity);
            }
        }
    }
}