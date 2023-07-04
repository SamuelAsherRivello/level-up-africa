using Unity.Entities;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This authoring component gets added to the player GameObject in the Roll A Ball sub scene.
    /// Authoring components define what ECS components get added to an entity during GameObject to Entity conversion
    /// Further we can define data values that will be set in the ECS components.
    /// </summary>
    public class PlayerAuthoring : MonoBehaviour
    {
        // We can set this field through the inspector in the Unity editor
        public float MoveForce;
    }

    /// <summary>
    /// The baker class is where we define which components and values are added to the converted entity
    /// </summary>
    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            // First we get a reference to the outputted entity. The Dynamic TransformUsageFlags mean that this entity
            // will have all the necessary transform components to move it in the game world.
            Entity playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            // Next we add components and tags to the entity as needed.
            AddComponent<PlayerTag>(playerEntity);
            // Here we reference the MoveForce value in the authoring class and set that data in the new component
            AddComponent(playerEntity, new PlayerMoveForce { Value = authoring.MoveForce });
        }
    }
}