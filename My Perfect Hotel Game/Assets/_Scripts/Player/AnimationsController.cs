using Events;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(PlayerWalkingStateChangedEvent))]
    public class AnimationsController : MonoBehaviour
    {
        private static readonly int IsWalking = Animator.StringToHash(nameof(IsWalking));
        
        private Animator _animator;
        private PlayerWalkingStateChangedEvent _playerWalkingStateChangedEvent;

        private void Awake()
        {
            _playerWalkingStateChangedEvent = GetComponent<PlayerWalkingStateChangedEvent>();
            _animator = GetComponent<Animator>();
        }

        private void Start() => 
            _playerWalkingStateChangedEvent.Event += OnPlayerWalkingStateChangedEvent;

        private void OnDestroy() =>
            _playerWalkingStateChangedEvent.Event -= OnPlayerWalkingStateChangedEvent;

        private void OnPlayerWalkingStateChangedEvent(object sender, bool isWalking) => 
            _animator.SetBool(IsWalking, isWalking);
    }
}
