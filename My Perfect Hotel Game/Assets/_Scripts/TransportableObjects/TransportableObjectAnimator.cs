using DG.Tweening;
using UnityEngine;

namespace TransportableObjects
{
    [RequireComponent(typeof(Transportable))]
    public class TransportableObjectAnimator : MonoBehaviour
    {
        private const float PICKUP_SPEED = 0.5f;
        private const float OFFSET = .85f;
     
        public void PickUp(int carryingObjectsCount)
        {
            Vector3 stackPosition = carryingObjectsCount == 1 
                ? Vector3.zero 
                : Vector3.up * ((carryingObjectsCount - 1) * OFFSET);
            
            transform.DOLocalMove(stackPosition, PICKUP_SPEED);
        }

        public void Drop()
        {
            Sequence mySequence = DOTween.Sequence();
            
            mySequence.Append(transform.DOLocalMove(Vector3.zero, PICKUP_SPEED));
            
            mySequence.Append(transform.DOScale(Vector3.zero, PICKUP_SPEED));
            
            mySequence.OnComplete(() => Destroy(gameObject));
        }
    }
}
