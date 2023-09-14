using System.Collections.Generic;
using Events;
using InteractableObject;
using UnityEngine;

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
        [SerializeField] private List<Interactable> _objectsToCleanList;

        private void Awake()
        {
            // Hardcoded for now, change later
            SetIsAvailable();
            LeftRoomEvent = GetComponent<GuestLeftRoomEvent>();
        }
        
        public Vector3 GetBedPosition()
            => _bedTransform.position;

        public void SetIsNotAvailable()
            => IsAvailable = false;

        public void SetIsAvailable()
            => IsAvailable = true;
    }
}
