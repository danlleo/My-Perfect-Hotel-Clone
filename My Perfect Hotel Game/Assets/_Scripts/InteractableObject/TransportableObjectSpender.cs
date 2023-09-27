using System;
using Player;
using TransportableObjects;
using UnityEngine;
using Utilities;

namespace InteractableObject
{
    [RequireComponent(typeof(TransformsToggler))]
    public class TransportableObjectSpender : Interactable
    {
        [SerializeField] private TransportableObjectSO _objectToSpend;
        [SerializeField] private Transform _destroyPoint;
        private bool _isCurrentlyRequiresTheObject = true;
        private TransformsToggler _transformsToggler;

        private void Awake()
        {
            _transformsToggler = GetComponent<TransformsToggler>();
            
            UpdateTapeVisual();
        }

        public override void Interact()
        {
            if (!_isCurrentlyRequiresTheObject) return;
            
            Inventory inventory = GameGlobalStorage.Instance.GetPlayer().GetInventory();
            
            if (inventory.GetCarryingObjectsCount() <= 0) 
                return;
            
            Transportable transportableObject = inventory.PeekCarryingObject();

            if (transportableObject.TransportableObject.Type != _objectToSpend.Type) 
                return;
            
            transportableObject.transform.SetParent(_destroyPoint);

            transportableObject.Use();

            _isCurrentlyRequiresTheObject = false;
            
            UpdateTapeVisual();
        }
        
        public override bool TryInteractWithCallback(out Action onComplete)
        {
            onComplete = () => _isCurrentlyRequiresTheObject = true;

            UpdateTapeVisual();
            
            return !_isCurrentlyRequiresTheObject;
        }

        private void UpdateTapeVisual() => _transformsToggler.SetActive(_isCurrentlyRequiresTheObject);
        
        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsNullValue(this, nameof(_objectToSpend), _objectToSpend);
            EditorValidation.IsNullValue(this, nameof(_destroyPoint), _destroyPoint);
        }
#endif

        #endregion
    }
}
