using Unity.Entities;

namespace TMG.RollABallDOTS
{
    public partial class PickupSFXSystem : SystemBase
    {
        private EntityQuery _pickupQuery;

        protected override void OnCreate()
        {
            RequireForUpdate<PickupAudioSourceTag>();
            _pickupQuery = SystemAPI.QueryBuilder().WithAll<PickedUpThisFrameTag, PickupTag>().Build();
            RequireForUpdate(_pickupQuery);
        }

        protected override void OnUpdate()
        {
            var pickupSFXEntity = SystemAPI.GetSingletonEntity<PickupAudioSourceTag>();
            var pickupSFX = EntityManager.GetComponentObject<PickupAudioSource>(pickupSFXEntity);
            pickupSFX.Value.Play();
        }
    }
}