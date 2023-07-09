﻿using Unity.Entities;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This component defines the strength of force that is applied to the player when moving through the world
    /// </summary>
    public struct PlayerMoveForce : IComponentData
    {
        public float Value;
    }
}