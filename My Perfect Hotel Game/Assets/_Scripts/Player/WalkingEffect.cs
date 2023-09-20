using Events;
using UnityEngine;
using Utilities;

namespace Player
{
    [RequireComponent(typeof(Player))]
    public class WalkingEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _movingParticleSystemEffect;
        [SerializeField] private Transform _stepsEffectPoint;
        
        private Player _player;
        private ParticleSystem _spawnedMovingParticleSystemEffect;
        
        private void Awake()
        {
            _player = GetComponent<Player>();
            _player.WalkingStateChangedEvent.Event += WalkingStateChanged_Event;
            
            SpawnEffect();
        }
        
        private void SpawnEffect()
        {
            ParticleSystem particle = Instantiate(_movingParticleSystemEffect, _stepsEffectPoint.position, _movingParticleSystemEffect.transform.rotation, _stepsEffectPoint);
            _spawnedMovingParticleSystemEffect = particle;
            _spawnedMovingParticleSystemEffect.Stop();
        }
        
        private void WalkingStateChanged_Event(object sender, PlayerWalkingStateChangedEventArgs e)
        {
            if (e.IsWalking)
            {
                if (!_spawnedMovingParticleSystemEffect.isPlaying)
                    _spawnedMovingParticleSystemEffect.Play();

                return;
            }
            
            _spawnedMovingParticleSystemEffect.Stop();
        }
        
        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsNullValue(this, nameof(_movingParticleSystemEffect), _movingParticleSystemEffect);
        }
#endif

        #endregion
    }
}
