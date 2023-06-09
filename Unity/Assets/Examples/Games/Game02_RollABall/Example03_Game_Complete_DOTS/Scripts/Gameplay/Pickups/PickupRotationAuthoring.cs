﻿using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This authoring component allows us to define the speed and direction a pickup will rotate. This script is added
    /// to a GameObject in the RollABallSubScene and the Baker will convert it to an entity at runtime.
    /// </summary>
    public class PickupRotationAuthoring : MonoBehaviour
    {
        public float Speed;
        public float3 Direction;

        /// <summary>
        /// This Baker sets up our pickup entity
        /// </summary>
        public class PickupRotationBaker : Baker<PickupRotationAuthoring>
        {
            public override void Bake(PickupRotationAuthoring authoring)
            {
                Entity pickupEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PickupTag>(pickupEntity);
                AddComponent(pickupEntity, new PickupRotation
                {
                    Direction = new float3
                    {
                        x = math.radians(authoring.Direction.x),
                        y = math.radians(authoring.Direction.y),
                        z = math.radians(authoring.Direction.z)
                    }, 
                    Speed = authoring.Speed
                });
            }
        }
    }
}