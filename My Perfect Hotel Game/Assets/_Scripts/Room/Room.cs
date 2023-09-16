using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using InteractableObject;
using StaticEvents.Room;
using UnityEngine;

namespace Room
{
    [SelectionBase]
    [RequireComponent(typeof(CustomerLeftRoomEvent))]
    [DisallowMultipleComponent]
    public class Room : MonoBehaviour
    {
        [HideInInspector] public CustomerLeftRoomEvent LeftRoomEvent;

        public bool IsAvailable { get; private set; }

        [SerializeField] private Transform _bedTransform;
        [SerializeField] private List<Interactable> _roomObjectList;

        private readonly HashSet<Interactable> _objectsToCleanHashSet = new();

        private Maid.Maid _maidOccupied;
        private Customer.Customer _customerOccupied;
        
        private void Awake()
        {
            // TODO: Hardcoded for now, change later
            LeftRoomEvent = GetComponent<CustomerLeftRoomEvent>();
            
            SetIsAvailable();
            ResetObjectsToCleanHashSetToDefault();
        }

        private void OnEnable()
        {
            LeftRoomEvent.Event += LeftRoom_Event;
        }

        private void OnDisable()
        {
            LeftRoomEvent.Event -= LeftRoom_Event;
        }

        public Vector3 GetBedPosition()
            => _bedTransform.position;

        public void OccupyRoomWithGuest(Customer.Customer customer)
        {
            _customerOccupied = customer;
            
            CleanObjectsToCleanHashSet();
            SetIsNotAvailable();
        }

        public void OccupyRoomWithMaid(Maid.Maid maid)
            => _maidOccupied = maid;

        public void TryFinishRoomCleaning(Interactable interactable)
        {
            _objectsToCleanHashSet.Add(interactable);
            
            if (_roomObjectList.Count != _objectsToCleanHashSet.Count) return;
            
            // If Room is indeed cleaned
            SetIsAvailable();
            RemoveMaidFromRoom();
        }

        public bool IsRoomUnclean()
            => _roomObjectList.Count != _objectsToCleanHashSet.Count;

        public bool HasMaidOccupied()
            => _maidOccupied != null;

        public bool HasGuestOccupied()
            => _customerOccupied != null;

        public Interactable GetUncleanObject()
            => _objectsToCleanHashSet.First();

        private void RemoveGuestFromRoom()
            => _customerOccupied = null;

        private void RemoveMaidFromRoom()
        {
            _maidOccupied.MaidRemovedFromRoomEvent.Call(this);
            _maidOccupied = null;
        }
        
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

        private void LeftRoom_Event(object sender, EventArgs e)
        {
            RemoveGuestFromRoom();
            ResetObjectsToCleanHashSetToDefault();
            RoomBecameAvailableToCleanStaticEvent.CallRoomBecameAvailableToCleanEvent(this);
        }
    }
}
