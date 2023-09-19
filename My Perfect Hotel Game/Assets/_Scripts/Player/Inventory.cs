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
        [Tooltip("Populate with initial spawn point on which object first object will be placed upon")]
        [SerializeField] private Transform _carryPointTransform;
        private bool _isCarrying;
        private Player _player;
        private readonly Stack<Transportable> _carryingObjectsStack = new();

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        public Transform GetCarryPoint() 
            => _carryPointTransform;
        
        public Transportable PeekCarryingObject() 
            => _carryingObjectsStack.Peek();
        
        public int GetCarryingObjectsCount() 
            => _carryingObjectsStack.Count;

        public void AddCarryingObject(Transportable transportable)
        {
            _carryingObjectsStack.Push(transportable);
            
            _isCarrying = GetCarryingObjectsCount() > 0;
            
            _player.PickedAnObjectEvent.Call(this);
        }

        public void RemoveCarryingObject()
        {
            _carryingObjectsStack.Pop();

            _isCarrying = GetCarryingObjectsCount() > 0;
            
            _player.DroppedAnObjectEvent.Call(this);
        }

        public bool IsCarrying() => _isCarrying;

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
