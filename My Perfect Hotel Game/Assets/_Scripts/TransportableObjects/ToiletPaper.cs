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
            
            transform.DOLocalMove(Vector3.zero, PICKUP_SPEED).OnComplete(() => Destroy(gameObject));
        }
    }
}
