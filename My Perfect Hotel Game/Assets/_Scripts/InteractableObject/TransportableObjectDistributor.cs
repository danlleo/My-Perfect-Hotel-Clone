using System;
using TransportableObjects;
using UnityEngine;

namespace InteractableObject
{
    public class TransportableObjectDistributor : Interactable
    {
        [SerializeField] private TransportableObjectSO _objectToSpawn;
        [SerializeField] private Transform _spawnPoint;

        [Tooltip("Populate with the time in seconds that will take Player to interact")] 
        [SerializeField] [Min(0.1f)] private float _interactTime = .35f;

        private float _timer;
        
        public override void Interact()
        {
            _timer += Time.deltaTime;

            if (_timer < _interactTime)
                return;
            
            Player.Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();

            if (inventory.IsCarrying())
            {
                print(inventory.PeekCarryingObject().TransportableObject);
                
                // Won't allow proceed if type is different from current carrying object
                if (inventory.PeekCarryingObject().TransportableObject.Type != _objectToSpawn.Type)
                {
                    ResetTimer();
                    return;   
                }
            }
            
            if (inventory.GetCarryingObjectsCount() >= _objectToSpawn.MaxCarryingAmount)
            {
                ResetTimer();
                return;
            }
            
            Transportable transportableObject = Instantiate(_objectToSpawn.Prefab, _spawnPoint.transform.position, Quaternion.identity);
            
            transportableObject.transform.SetParent(inventory.GetCarryPoint());
            
            transportableObject.PickUp();
            
            ResetTimer();
        }

        public override bool TryInteractWithCallback(out Action onComplete) 
            => throw new NotImplementedException();

        private void ResetTimer()
            => _timer = 0f;
    }
}
