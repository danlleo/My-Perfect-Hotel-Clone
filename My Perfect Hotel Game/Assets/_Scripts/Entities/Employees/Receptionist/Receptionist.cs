using System;
using InteractableObject;
using UnityEngine;
using Utilities;

namespace Entities.Employees.Receptionist
{
    public class Receptionist : Employee
    {
        [SerializeField] private Interactable _reception;

        private void Update()
        {
            if (!_reception.TryInteractWithCallback(out Action onComplete))
                return;
            
            onComplete?.Invoke();
        }

        protected override Vector3 GetNextDestination()
        {
            throw new System.NotImplementedException();
        }
        
        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsNullValue(this, nameof(_reception), _reception);
        }
#endif

        #endregion
    }
}
