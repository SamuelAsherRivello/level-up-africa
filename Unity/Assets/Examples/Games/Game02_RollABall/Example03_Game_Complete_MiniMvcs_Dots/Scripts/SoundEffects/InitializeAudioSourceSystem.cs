using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    [RequireMatchingQueriesForUpdate]
    public partial class InitializeAudioSourceSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var (tag, entity) in SystemAPI.Query<PickupAudioSourceTag>().WithEntityAccess().WithNone<PickupAudioSource>())
            {
                var sfxObject = new GameObject("SFX Source");
                var audioSource = sfxObject.AddComponent<AudioSource>();
                audioSource.clip = Resources.Load<AudioClip>("DOTSAudioClips/Bounce01");
                audioSource.playOnAwake = false;
                ecb.AddComponent(entity, new PickupAudioSource { Value = audioSource });
            }
            
            foreach (var (audioSource, entity) in SystemAPI.Query<PickupAudioSource>().WithEntityAccess().WithNone<PickupAudioSourceTag>())
            {
                if (audioSource.Value != null)
                {
                    Object.Destroy(audioSource.Value.gameObject);
                }
                ecb.RemoveComponent<PickupAudioSource>(entity);
            }
            
            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
    
    [RequireMatchingQueriesForUpdate]
    public partial class CleanupAudioSourceSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var (audioSource, entity) in SystemAPI.Query<PickupAudioSource>().WithEntityAccess().WithNone<PickupAudioSourceTag>())
            {
                if (audioSource.Value != null)
                {
                    Object.Destroy(audioSource.Value.gameObject);
                }
                ecb.RemoveComponent<PickupAudioSource>(entity);
            }
            
            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}