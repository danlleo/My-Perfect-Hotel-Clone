using System;
using Player;
using TransportableObjects;
using UnityEngine;
using Utilities;

namespace InteractableObject
{
    [SelectionBase]
    public class TransportableObjectDistributor : Interactable
    {
        [Tooltip("Populate with the time in seconds that will take Player to interact")] 
        [SerializeField] [Min(0.1f)] private float _interactTime = .35f;
        [SerializeField] private TransportableObjectSO _objectToSpawn;
        [SerializeField] private Transform _spawnPoint;
        
        private float _timer;
        
        public override void Interact()
        {
            _timer += Time.deltaTime;

            if (_timer < _interactTime)
                return;
            
            Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();

            if (inventory.IsCarrying())
            {
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
        
        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsPositiveValue(this, nameof(_interactTime), _interactTime);
            EditorValidation.IsNullValue(this, nameof(_objectToSpawn), _objectToSpawn);
            EditorValidation.IsNullValue(this, nameof(_spawnPoint), _spawnPoint);
        }
#endif

        #endregion
    }
}
