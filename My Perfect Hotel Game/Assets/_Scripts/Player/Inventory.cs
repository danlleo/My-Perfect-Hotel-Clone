using System.Collections.Generic;
using TransportableObjects;
using UnityEngine;
using Utilities;

namespace Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Player))]
    public class Inventory : MonoBehaviour
    {
        public const int MAX_CARRY_COUNT = 3;
        
        [SerializeField] private Transform _carryPointTransform;
        private readonly Stack<Transportable> _carryingObjectsList = new();
        
        public Transform GetCarryPoint() => _carryPointTransform;

        public Transportable GetCarryingObject() => _carryingObjectsList.Peek();
        
        public int GetCarryingObjectsCount() => _carryingObjectsList.Count;

        public void AddCarryingObject(Transportable transportable) => _carryingObjectsList.Push(transportable);

        public void RemoveCarryingObject() => _carryingObjectsList.Pop();

        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsNullValue(this, nameof(_carryPointTransform), _carryPointTransform);
        }
#endif

        #endregion
    }
}
