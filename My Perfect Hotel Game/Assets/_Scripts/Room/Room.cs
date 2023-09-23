using System;
using System.Collections.Generic;
using Entities.Customer;
using Enums;
using Events;
using InteractableObject;
using StaticEvents.CameraView;
using StaticEvents.Room;
using UnityEngine;
using Utilities;

namespace Room
{
    [SelectionBase]
    [RequireComponent(typeof(TransformsToggler))]
    [RequireComponent(typeof(CustomerLeftRoomEvent))]
    [RequireComponent(typeof(RoomObjectCleanedEvent))]
    [DisallowMultipleComponent]
    public class Room : MonoBehaviour
    {
        public CustomerLeftRoomEvent LeftRoomEvent { get; private set; }
        public RoomObjectCleanedEvent ObjectCleanedEvent { get; private set; }

        public bool IsAvailable { get; private set; }
        public bool HasMaidOccupied { get; private set; }
        
        [SerializeField] private Transform _bedTransform;
        [SerializeField] private RoomDirection _roomDirection;
        [SerializeField] private List<Interactable> _roomObjectList;
        
        private readonly Dictionary<Interactable, bool> _objectsToCleanDictionary = new();

        private Customer _customerOccupied;
        
        private void Awake()
        {
            LeftRoomEvent = GetComponent<CustomerLeftRoomEvent>();
            ObjectCleanedEvent = GetComponent<RoomObjectCleanedEvent>();
            
            SetIsAvailable();
            CopyItemsFromListToDictionary();
        }

        private void OnEnable()
        {
            LeftRoomEvent.Event += CustomerLeftRoom_Event;
        }

        private void OnDisable()
        {
            LeftRoomEvent.Event -= CustomerLeftRoom_Event;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
                CameraViewChangedStaticEvent.CallCameraViewChangedEvent(true, _roomDirection);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
                CameraViewChangedStaticEvent.CallCameraViewChangedEvent(false, _roomDirection);
        }

        public Vector3 GetBedPosition()
            => _bedTransform.position;

        public void OccupyRoomWithGuest(Customer customer)
        {
            _customerOccupied = customer;
            SetIsNotAvailable();
        }

        public void OccupyRoomWithMaid()
            => HasMaidOccupied = true;

        public void TryFinishRoomCleaning(Interactable interactable)
        {
            SetItemAsCleaned(interactable);
            
            if (IsRoomUnclean()) return;
            
            // If Room is indeed cleaned, perform actions below
            SetIsAvailable();

            if (!HasMaidOccupied)
                return;
            
            RoomCleanedStaticEvent.CallRoomCleanedEvent(this);
            RemoveMaidFromRoom();
        }

        public bool IsRoomUnclean()
        {
            foreach (KeyValuePair<Interactable, bool> keyValuePair in _objectsToCleanDictionary)
            {
                if (!_objectsToCleanDictionary.TryGetValue(keyValuePair.Key, out bool isClean)) continue;
                
                if (!isClean)
                    return true;
            }

            return false;
        }
        
        public bool TryGetUncleanObject(out Interactable uncleanObject)
        {
            uncleanObject = null;
            
            foreach (Interactable interactable in _roomObjectList)
            {
                if (!_objectsToCleanDictionary.TryGetValue(interactable, out bool isClean)) continue;

                if (isClean) continue;
                
                uncleanObject = interactable;
                return true;
            }

            return false;
        }

        private void RemoveCustomerFromRoom()
            => _customerOccupied = null;

        private void RemoveMaidFromRoom()
            => HasMaidOccupied = false;
        
        private void SetIsNotAvailable()
            => IsAvailable = false;
        
        private void SetIsAvailable()
            => IsAvailable = true;

        private void ResetObjectsToUncleanDictionary()
        {
            foreach (Interactable interactable in _roomObjectList)
                _objectsToCleanDictionary[interactable] = false;
        }

        private void CopyItemsFromListToDictionary()
        {
            foreach (Interactable interactable in _roomObjectList)
                _objectsToCleanDictionary.Add(interactable, true);
        }

        private void SetItemAsCleaned(Interactable interactable)
        {
            _objectsToCleanDictionary[interactable] = true;
            ObjectCleanedEvent.Call(this, new RoomObjectCleanedEventArgs(interactable));
        }

        #region Events

        private void CustomerLeftRoom_Event(object sender, EventArgs e)
        {
            RemoveCustomerFromRoom();
            ResetObjectsToUncleanDictionary();
            RoomBecameAvailableToCleanStaticEvent.CallRoomBecameAvailableToCleanEvent(this);
        }

        #endregion

        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsNullValue(this, nameof(_bedTransform), _bedTransform);
            EditorValidation.AreEnumerableValues(this, nameof(_roomObjectList), _roomObjectList);
        }
#endif

        #endregion
    }
}