using System.Collections.Generic;
using Events;
using InteractableObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace Room
{
    [SelectionBase]
    [RequireComponent(typeof(GuestLeftRoomEvent))]
    [DisallowMultipleComponent]
    public class Room : MonoBehaviour
    {
        [HideInInspector] public GuestLeftRoomEvent LeftRoomEvent;

        public bool IsAvailable { get; private set; }

        [SerializeField] private Transform _bedTransform;
        [SerializeField] private List<Interactable> _roomObjectList;

        private HashSet<Interactable> _objectsToCleanHashSet = new();
        
        private void Awake()
        {
            // TODO: Hardcoded for now, change later
            SetIsAvailable();
            LeftRoomEvent = GetComponent<GuestLeftRoomEvent>();
        }
        
        public Vector3 GetBedPosition()
            => _bedTransform.position;

        public void SetIsNotAvailable()
            => IsAvailable = false;

        public void SetIsAvailable()
            => IsAvailable = true;

        public void TryFinishRoomCleaning(Interactable interactable)
        {
            _objectsToCleanHashSet.Add(interactable);

            if (_roomObjectList.Count == _objectsToCleanHashSet.Count)
                SetIsAvailable();
        }
    }
}
