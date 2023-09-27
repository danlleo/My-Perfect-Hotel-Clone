using System;
using Player;
using TransportableObjects;
using UnityEngine;
using Utilities;

namespace InteractableObject
{
    public class TransportableObjectDestroyer : Interactable
    {
        [Tooltip("Populate with the time in seconds that will take Player to interact")] 
        [SerializeField] [Min(0.1f)] private float _interactTime = .35f;
        [SerializeField] private Transform _destroyPoint;

        private float _timer;
        
        public override void Interact()
        {
            _timer += Time.deltaTime;
            
            if (_timer < _interactTime)
                return;
            
            Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();
            
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
        
        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsPositiveValue(this, nameof(_interactTime), _interactTime);
            EditorValidation.IsNullValue(this, nameof(_destroyPoint), _destroyPoint);
        }
#endif

        #endregion
    }
}
