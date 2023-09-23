using InteractableObject;
using TransportableObjects;
using UnityEngine;
using Utilities;

namespace Player
{
    [DisallowMultipleComponent]
    public class Interact : MonoBehaviour
    {
        [Space(10)]
        [Header("Interact Dependencies")]
        [Space(5)]

        [SerializeField] private Transform _detectInteractableObjectPoint;

        [Space(10)]
        [Header("Interact Params")]
        [Space(5)]
        
        [Tooltip("Populate with the percentage value that will represent availability to perform actions if vectors are similar")]
        [SerializeField] private float _threshold = 0.925f;
        
        [Tooltip("Populate with the value of interact distance")]
        [SerializeField] private float _interactDistance = 0.25f;

        private Interactable _interactable;
        private Transportable _transportable;

        private void Update()
            => PerformInteract();
        
       private void PerformInteract()
       {
            var ray = new Ray(_detectInteractableObjectPoint.position, _detectInteractableObjectPoint.forward);
            
            foreach (Interactable interactableObject in InteractableObjectsStorage.Instance.GetInteractableObjects())
            {
                Vector3 directionFromRayOriginToSelectableObject3D = interactableObject.transform.position - ray.origin;
                
                var rayDirection = new Vector2(ray.direction.x, ray.direction.z);
                var directionFromRayOriginToSelectableObject2D = new Vector2(directionFromRayOriginToSelectableObject3D.x, directionFromRayOriginToSelectableObject3D.z);

                float lookPercentage = Vector2.Dot(rayDirection.normalized, directionFromRayOriginToSelectableObject2D.normalized);

                if (!(lookPercentage > _threshold)) continue;

                if (!(Vector2.Distance(transform.position, interactableObject.transform.position) <=
                      _interactDistance)) continue;
                
                _interactable = interactableObject;
                _interactable.Interact();
            }
       }

       #region Validation

#if UNITY_EDITOR
       private void OnValidate()
       {
           EditorValidation.IsPositiveRange(
               this, 
               nameof(_threshold),
               _threshold, 
               nameof(_threshold),
               1f);
           EditorValidation.IsPositiveValue(this, nameof(_interactDistance), _interactDistance);
       }
#endif

       #endregion
    }
}