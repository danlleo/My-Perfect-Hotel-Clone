using Player;
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
            Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();
            
            inventory.AddCarryingObject(this);

            int carryingObjectsCount = inventory.GetCarryingObjectsCount();
            
            _objectAnimator.PickUp(carryingObjectsCount);
        }
        
        public void Drop()
        {
            Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();

            inventory.RemoveCarryingObject();

            _objectAnimator.Drop(() => Destroy(gameObject));
        }

        public void Use()
        {
            Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();

            inventory.RemoveCarryingObject();
            
            _objectAnimator.Use(() => Destroy(gameObject));
        }
        
        public TransportableObjectSO TransportableObject => _transportableObject;
    }
}
