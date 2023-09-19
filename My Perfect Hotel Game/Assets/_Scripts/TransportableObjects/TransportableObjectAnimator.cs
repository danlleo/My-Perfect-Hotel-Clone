using System;
using DG.Tweening;
using Events;
using UnityEngine;

namespace TransportableObjects
{
    [RequireComponent(typeof(Transportable))]
    [RequireComponent(typeof(PlayerMadeAStep))]
    public class TransportableObjectAnimator : MonoBehaviour
    {
        private const float PICKUP_SPEED = 0.5f;
        private const float OFFSET = .85f;
        private const float JUMP_DURATION = 0.3f;
        private const float JUMP_POWER_MULTIPLIER = 0.02f;
        
        private Player.Player _player;
        private bool _isPickUpAnimationPlaying;
        private bool _isDropAnimationPlaying;
        
        private void Awake() => 
            _player = GameGlobalStorage.Instance.GetPlayer();

        private void OnEnable() => 
            _player.StepEvent.Event += Player_OnMadeAStep;

        private void OnDisable() => 
            _player.StepEvent.Event -= Player_OnMadeAStep;

        public void PickUp(int carryingObjectsCount)
        {
            _isPickUpAnimationPlaying = true;
            
            Vector3 stackPosition = carryingObjectsCount == 1 
                ? Vector3.zero 
                : Vector3.up * ((carryingObjectsCount - 1) * OFFSET);
            
            transform.DOLocalMove(stackPosition, PICKUP_SPEED)
                .OnComplete(() => { _isPickUpAnimationPlaying = false; });
        }

        public void Drop()
        {
            _isDropAnimationPlaying = true;
                
            Sequence mySequence = DOTween.Sequence();
            
            mySequence.Append(transform.DOLocalMove(Vector3.zero, PICKUP_SPEED));
            
            mySequence.Append(transform.DOScale(Vector3.zero, PICKUP_SPEED));
            
            mySequence.OnComplete(() =>
            {
                _isDropAnimationPlaying = false;
                Destroy(gameObject);
            });
        }

        private void Jump()
        {
            if (_isPickUpAnimationPlaying) return;
            if (_isDropAnimationPlaying) return;
            
            float jumpPower = Mathf.Exp(transform.localPosition.y) * JUMP_POWER_MULTIPLIER;
            
            transform.DOLocalJump(transform.localPosition, jumpPower, 1, JUMP_DURATION);
        }
        
        private void Player_OnMadeAStep(object sender, EventArgs e) => 
            Jump();
    }
}
