using DG.Tweening;
using UnityEngine;

namespace TransportableObjects
{
    public class ToiletPaper : Transportable
    {
        public override void PickUp()
        {
            Player.Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();
            
            inventory.AddCarryingObject(this);
            
            transform.DOLocalMove(Vector3.zero, PICKUP_SPEED);
        }
        
        public override void Drop()
        {
            Player.Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();

            inventory.RemoveCarryingObject();

            Sequence mySequence = DOTween.Sequence();
            
            mySequence.Append(transform.DOLocalMove(Vector3.zero, PICKUP_SPEED));
            
            mySequence.Append(transform.DOScale(Vector3.zero, PICKUP_SPEED));
            
            mySequence.OnComplete(() => Destroy(gameObject));
        }
    }
}
