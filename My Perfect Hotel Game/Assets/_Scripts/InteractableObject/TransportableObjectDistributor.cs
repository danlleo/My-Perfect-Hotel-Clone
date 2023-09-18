using System;
using TransportableObjects;
using UnityEngine;

namespace InteractableObject
{
    public class TransportableObjectDistributor : Interactable
    {
        [SerializeField] private Transportable _objectToSpawn;
        [SerializeField] private Transform _spawnPoint;

        public override void Interact()
        {
            Player.Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();
            
            if (inventory.GetCarryingObjectsCount() >= Player.Inventory.MAX_CARRY_COUNT)
                return;
            
            Transportable transportableObject = Instantiate(_objectToSpawn, _spawnPoint.transform.position, Quaternion.identity);
            
            transportableObject.transform.SetParent(inventory.GetCarryPoint());

            transportableObject.PickUp();
        }

        public override bool TryInteractWithCallback(out Action onComplete) => throw new NotImplementedException();
    }
}
