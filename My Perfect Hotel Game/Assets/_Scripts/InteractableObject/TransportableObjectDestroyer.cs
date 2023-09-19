using System;
using TransportableObjects;
using UnityEngine;

namespace InteractableObject
{
    public class TransportableObjectDestroyer : Interactable
    {
        [SerializeField] private Transform _destroyPoint;
        
        [Tooltip("Populate with the time in seconds that will take Player to interact")] 
        [SerializeField] [Min(0.1f)] private float _interactTime = .35f;

        private float _timer;
        
        public override void Interact()
        {
            _timer += Time.deltaTime;
            
            if (_timer < _interactTime)
                return;
            
            Player.Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();
            
            if (inventory.GetCarryingObjectsCount() <= 0)
            {
                ResetTimer();
                return;
            }

            Transportable transportableObject = inventory.PeekCarryingObject();
            transportableObject.transform.SetParent(_destroyPoint);

            transportableObject.Drop();
            ResetTimer();
        }
        
        public override bool TryInteractWithCallback(out Action onComplete) 
            => throw new NotImplementedException();
        
        private void ResetTimer()
            => _timer = 0f;
    }
}
