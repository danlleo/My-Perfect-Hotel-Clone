using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimationsController : MonoBehaviour
    {
        private Animator _animator;
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _animator = GetComponent<Animator>();
        }
        
        private void OnEnable() => 
            _player.WalkingStateChangedEvent.Event += OnWalkingStateChangedEvent;
        
        private void OnDisable() => 
            _player.WalkingStateChangedEvent.Event -= OnWalkingStateChangedEvent;

        private void OnDestroy() =>
            _player.WalkingStateChangedEvent.Event -= OnWalkingStateChangedEvent;

        private void OnWalkingStateChangedEvent(object sender, bool isWalking) => 
            _animator.SetBool(AnimationsParams.IsWalking, isWalking);
    }
}
