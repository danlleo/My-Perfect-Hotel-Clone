using DG.Tweening;
using UnityEngine;

namespace TransportableObjects
{
    public class Transportable : MonoBehaviour
    {
        private const float PICKUP_SPEED = 0.5f;
        private const float OFFSET = .85f;
        
        [SerializeField] private TransportableObjectSO _transportableObject;
        
        public void PickUp()
        {
            Player.Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();
            
            inventory.AddCarryingObject(this);

            int carryingObjectsCount = inventory.GetCarryingObjectsCount();
            
            Vector3 stackPosition = carryingObjectsCount == 1 
                ? Vector3.zero 
                : Vector3.up * ((carryingObjectsCount - 1) * OFFSET);
            
            transform.DOLocalMove(stackPosition, PICKUP_SPEED);
        }
        
        public void Drop()
        {
            Player.Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();

            inventory.RemoveCarryingObject();

            Sequence mySequence = DOTween.Sequence();
            
            mySequence.Append(transform.DOLocalMove(Vector3.zero, PICKUP_SPEED));
            
            mySequence.Append(transform.DOScale(Vector3.zero, PICKUP_SPEED));
            
            mySequence.OnComplete(() => Destroy(gameObject));
        }
        
        public TransportableObjectSO TransportableObject => _transportableObject;
    }
}
