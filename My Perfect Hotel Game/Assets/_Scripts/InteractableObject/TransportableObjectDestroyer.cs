using System;
using TransportableObjects;
using UnityEngine;

namespace InteractableObject
{
    public class TransportableObjectDestroyer : Interactable
    {
        [SerializeField] private Transform _destroyPoint;
        
        public override void Interact()
        {
            Player.Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();
            
            if (inventory.GetCarryingObjectsCount() <= 0)
                return;

            Transportable transportableObject = inventory.GetCarryingObject();
            transportableObject.transform.SetParent(_destroyPoint);

            transportableObject.Drop();
        }
        
        public override bool TryInteractWithCallback(out Action onComplete) => throw new NotImplementedException();
    }
}
