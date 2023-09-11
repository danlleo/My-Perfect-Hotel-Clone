using System.Collections.Generic;
using InteractableObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [DisallowMultipleComponent]
    public class Interact : MonoBehaviour
    {
        [Space(10)]
        [Header("Interact Dependencies")]
        [Space(5)]
        
        [Tooltip("Populate with transform point from which we will shoot ray from to detect interactable objects")]
        [FormerlySerializedAs("_detectInterctableObjectPoint")] [SerializeField] private Transform _detectInteractableObjectPoint;
        
        [Tooltip("Populate list with objects that will be possible to interact with")]
        [SerializeField] private List<Interactable> _interactableObjectList;
        
        [Space(10)]
        [Header("Interact Params")]
        [Space(5)]
        
        [Tooltip("Populate with the percentage value that will represent availability to perform actions if vectors are similar")]
        [SerializeField] private float _threshold = 0.925f;
        
        [Tooltip("Populate with the value of interact distance")]
        [SerializeField] private float _interactDistance = 0.25f;

        private Interactable _interactable;

        private void Update()
        {
            PerformInteract();
        }

       private void PerformInteract()
        {
            var ray = new Ray(_detectInteractableObjectPoint.position, _detectInteractableObjectPoint.forward);
            float closest = 0;
            
            foreach (var interactableObject in _interactableObjectList)
            {
                var rayDirection = ray.direction;
                var directionFromRayOriginToSelectableObject = interactableObject.transform.position - ray.origin;

                float lookPercentage = Vector3.Dot(rayDirection.normalized, directionFromRayOriginToSelectableObject.normalized);

                if (!(lookPercentage > _threshold) || !(lookPercentage > closest)) continue;
                
                closest = lookPercentage;
                _interactable = interactableObject;

                if (Vector3.Distance(transform.position, interactableObject.transform.position) >=
                    _interactDistance)
                {
                    _interactable.Interact();
                }
            }
        }
    }
}