using System.Collections.Generic;
using InteractableObject;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class Interact : MonoBehaviour
    {
        [SerializeField] private List<InteractableObject.InteractableObject> _selectableObjectList;
        [SerializeField] private float _threshold = 0.925f;
        [SerializeField] private float _interactDistance = 0.25f;

        private InteractableObject.InteractableObject _interactableObject;
        
        public void Check(Ray ray)
        {
            
            float closest = 0;
            
            for (int i = 0; i < _selectableObjectList.Count; i++)
            {
                var rayDirection = ray.direction;
                var directionFromRayOriginToSelectableObject = _selectableObjectList[i].transform.position - ray.origin;

                float lookPercentage = Vector3.Dot(rayDirection.normalized, directionFromRayOriginToSelectableObject.normalized);
                
                _selectableObjectList[i].SetLookPercentage(lookPercentage);

                if (!(lookPercentage > _threshold) || !(lookPercentage > closest)) continue;
                
                closest = lookPercentage;
                _interactableObject = _selectableObjectList[i];
            }
        }
    }
}