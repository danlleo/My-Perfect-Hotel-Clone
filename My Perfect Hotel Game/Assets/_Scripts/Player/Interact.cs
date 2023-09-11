using System.Collections.Generic;
using InteractableObject;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    public class Interact : MonoBehaviour, ISelector
    {
        [SerializeField] private List<Selectable> _selectableObjectList;
        [SerializeField] private float _threshold = 0.925f;
        
        private Selectable _selection;
        
        public void Check(Ray ray)
        {
            _selection = null;

            float closest = 0;
            
            for (int i = 0; i < _selectableObjectList.Count; i++)
            {
                var vector1 = ray.direction;
                var vector2 = _selectableObjectList[i].transform.position - ray.origin;

                float lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);
                
                _selectableObjectList[i].SetLookPercentage(lookPercentage);

                if (!(lookPercentage > _threshold) || !(lookPercentage > closest)) continue;
                
                closest = lookPercentage;
                _selection = _selectableObjectList[i];
            }
        }

        public Selectable GetSelected()
            => _selection;
    }
}