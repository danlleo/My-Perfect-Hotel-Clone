using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private List<Interactable> _roomObjectList;

        private HashSet<Interactable> _objectsToCleanHashSet = new();

        private Maid.Maid _maidOccupied;
        
        private void Awake()
        {
            // TODO: Hardcoded for now, change later
            LeftRoomEvent = GetComponent<GuestLeftRoomEvent>();
            
            SetIsAvailable();
            ResetObjectsToCleanHashSetToDefault();
        }
        
        public Vector3 GetBedPosition()
            => _bedTransform.position;

        public void OccupyRoomWithGuest()
        {
            CleanObjectsToCleanHashSet();
            SetIsNotAvailable();
        }

        public void OccupyRoomWithMaid(Maid.Maid maid)
            => _maidOccupied = maid;

        public void TryFinishRoomCleaning(Interactable interactable)
        {
            _objectsToCleanHashSet.Add(interactable);

            if (_roomObjectList.Count == _objectsToCleanHashSet.Count)
                SetIsAvailable();
        }

        public bool IsRoomUnclean()
            => _roomObjectList.Count != _objectsToCleanHashSet.Count;

        public bool HasMaidOccupied()
            => _maidOccupied != null;

        public Interactable GetUncleanObject()
            => _objectsToCleanHashSet.First();
        
        private void SetIsNotAvailable()
            => IsAvailable = false;
        
        private void SetIsAvailable()
            => IsAvailable = true;

        private void ResetObjectsToCleanHashSetToDefault()
        {
            foreach (var roomItem in _roomObjectList)
                _objectsToCleanHashSet.Add(roomItem);
        }

        private void CleanObjectsToCleanHashSet()
            => _objectsToCleanHashSet.Clear();
    }
}
