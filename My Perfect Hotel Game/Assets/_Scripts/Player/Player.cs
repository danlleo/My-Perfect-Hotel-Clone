using System.Collections.Generic;
using Events;
using JetBrains.Annotations;
using TransportableObjects;
using UnityEngine;
using Utilities;

namespace Player
{
    [SelectionBase]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Interact))]
    [RequireComponent(typeof(AnimationsController))]
    [RequireComponent(typeof(PlayerWalkingStateChangedEvent))]
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        public const int MAX_CARRY_COUNT = 3;
        public const float RADIUS = .5f;
        public const float HEIGHT = 1f;
        
        [HideInInspector] public PlayerWalkingStateChangedEvent WalkingStateChangedEvent;

        [SerializeField] private Transform _carryPointTransform;
        [NotNull] private List<Transportable> _carryingObjectsList = new();

        private void Awake()
        {
            WalkingStateChangedEvent = GetComponent<PlayerWalkingStateChangedEvent>();
        }

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
