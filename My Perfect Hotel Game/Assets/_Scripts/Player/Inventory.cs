#nullable enable
using System.Collections.Generic;
using JetBrains.Annotations;
using TransportableObjects;
using UnityEngine;
using Utilities;

namespace Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Player))]
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Transform _carryPointTransform;
        [NotNull] private List<Transportable> _carryingObjectsList = new();
        
        public Transform GetCarryPoint() => _carryPointTransform;

        public Transportable GetCarryingObject() => _carryingObjectsList[^1];
        
        public int GetCarryingObjectsCount() => _carryingObjectsList.Count;

        public void AddCarryingObject(Transportable transportable) => _carryingObjectsList.Add(transportable);

        public void RemoveCarryingObject() => _carryingObjectsList.RemoveAt(_carryingObjectsList.Count - 1);

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
