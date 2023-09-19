using UnityEngine;

namespace TransportableObjects
{
    [RequireComponent(typeof(TransportableObjectAnimator))]
    public class Transportable : MonoBehaviour
    {
        [SerializeField] private TransportableObjectSO _transportableObject;
        private TransportableObjectAnimator _objectAnimator;

        private void Awake()
        {
            _objectAnimator = GetComponent<TransportableObjectAnimator>();
        }

        public void PickUp()
        {
            Player.Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();
            
            inventory.AddCarryingObject(this);

            int carryingObjectsCount = inventory.GetCarryingObjectsCount();
            
            _objectAnimator.PickUp(carryingObjectsCount);
        }
        
        public void Drop()
        {
            Player.Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();

            inventory.RemoveCarryingObject();

            _objectAnimator.Drop();
        }
        
        public TransportableObjectSO TransportableObject => _transportableObject;
    }
}
