using System;
using DG.Tweening;
using Events;
using UnityEngine;

namespace TransportableObjects
{
    [RequireComponent(typeof(Transportable))]
    public class TransportableObjectAnimator : MonoBehaviour
    {
        private const float PICKUP_SPEED = 0.5f;
        private const float OFFSET = .85f;
        
        private Player.Player _player;
        
        private void Awake() => 
            _player = GameGlobalStorage.Instance.GetPlayer();

        public void PickUp(int carryingObjectsCount)
        {
            transform.DOComplete();
            
            Vector3 stackPosition = carryingObjectsCount == 1 
                ? Vector3.zero 
                : Vector3.up * ((carryingObjectsCount - 1) * OFFSET);

            transform.DOLocalMove(stackPosition, PICKUP_SPEED);
        }

        public void Drop(Action onComplete)
        {
            transform.DOComplete();

            Sequence mySequence = DOTween.Sequence();
            
            mySequence.Append(transform.DOLocalMove(Vector3.zero, PICKUP_SPEED));
            
            mySequence.Append(transform.DOScale(Vector3.zero, PICKUP_SPEED));
            
            mySequence.OnComplete(() => onComplete());
        }
        
        public void Use(Action onComplete)
        {
            transform.DOComplete();
            
            Sequence mySequence = DOTween.Sequence();
            
            mySequence.Append(transform.DOLocalMove(Vector3.zero, PICKUP_SPEED));
            
            mySequence.Append(transform.DOScale(Vector3.zero, PICKUP_SPEED));
            
            mySequence.OnComplete(() => onComplete());
        }
    }
}
