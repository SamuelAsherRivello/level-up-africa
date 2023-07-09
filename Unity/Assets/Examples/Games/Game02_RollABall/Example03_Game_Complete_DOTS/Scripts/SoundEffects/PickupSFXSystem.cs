using Unity.Entities;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This system plays the pickup sound effect when a pickup has been picked up
    /// </summary>
    public partial class PickupSFXSystem : SystemBase
    {
        // This queries for all entities that have been picked up this frame.
        private EntityQuery _pickupQuery;

        protected override void OnCreate()
        {
            // Ensure necessary entities exist before updating
            RequireForUpdate<PickupAudioSourceTag>();
            _pickupQuery = SystemAPI.QueryBuilder().WithAll<PickedUpThisFrameTag, PickupTag>().Build();
            RequireForUpdate(_pickupQuery);
        }

        protected override void OnUpdate()
        {
            // Play the sound effect. This OnUpdate method will only run when we have a newly picked up entity due to
            // how the RequireForUpdates are used
            var pickupSFXEntity = SystemAPI.GetSingletonEntity<PickupAudioSourceTag>();
            var pickupSFX = EntityManager.GetComponentObject<PickupAudioSource>(pickupSFXEntity);
            pickupSFX.Value.Play();
        }
    }
}