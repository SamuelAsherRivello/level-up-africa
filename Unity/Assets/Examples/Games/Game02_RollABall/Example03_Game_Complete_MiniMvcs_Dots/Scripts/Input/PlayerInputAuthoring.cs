using Unity.Entities;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    public class PlayerInputAuthoring : MonoBehaviour
    {
    }

    public class PlayerInputAuthoringBaker : Baker<PlayerInputAuthoring>
    {
        public override void Bake(PlayerInputAuthoring authoring)
        {
            Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerMoveInput>(inputEntity);
        }
    }
}