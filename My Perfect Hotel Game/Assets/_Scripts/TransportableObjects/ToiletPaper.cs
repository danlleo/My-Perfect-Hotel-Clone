using DG.Tweening;
using UnityEngine;

namespace TransportableObjects
{
    public class ToiletPaper : Transportable
    {
        public override void PickUp()
        {
            Player.Player player = GameGlobalStorage.Instance.GetPlayer();
            
            player.AddCarryingObject(this);
            
            transform.DOLocalMove(Vector3.zero, PICKUP_SPEED);
        }
        
        public override void Drop()
        {
            Player.Player player = GameGlobalStorage.Instance.GetPlayer();

            player.RemoveCarryingObject();
            
            transform.DOLocalMove(Vector3.zero, PICKUP_SPEED).OnComplete(() => Destroy(gameObject));
        }
    }
}
