using System;
using System.Collections.Generic;
using Entities.Customer;
using Entities.Employees.Maid;
using Events;
using InteractableObject;
using StaticEvents.Room;
using UnityEngine;
using Utilities;

namespace Room
{
    [SelectionBase]
    [RequireComponent(typeof(CustomerLeftRoomEvent))]
    [RequireComponent(typeof(RoomObjectCleanedEvent))]
    [DisallowMultipleComponent]
    public class Room : MonoBehaviour
    {
        public CustomerLeftRoomEvent LeftRoomEvent { get; private set; }
        public RoomObjectCleanedEvent ObjectCleanedEvent { get; private set; }

        public bool IsAvailable { get; private set; }
        
        [SerializeField] private Transform _bedTransform;
        [SerializeField] private List<Interactable> _roomObjectList;

        private readonly Dictionary<Interactable, bool> _objectsToCleanDictionary = new();

        private Maid _maidOccupied;
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

        public Vector3 GetBedPosition()
            => _bedTransform.position;

        public void OccupyRoomWithGuest(Customer customer)
        {
            _customerOccupied = customer;
            SetIsNotAvailable();
        }

        public void OccupyRoomWithMaid(Maid maid)
            => _maidOccupied = maid;

        public void TryFinishRoomCleaning(Interactable interactable)
        {
            SetItemAsCleaned(interactable);
            
            if (IsRoomUnclean()) return;
            
            // If Room is indeed cleaned, perform actions below
            SetIsAvailable();

            if (!HasMaidOccupied())
                return;
            
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

        public bool HasMaidOccupied()
            => _maidOccupied != null;

        public bool HasCustomerOccupied()
            => _customerOccupied != null;

        public Interactable TryGetUncleanObject()
        {
            foreach (Interactable interactable in _roomObjectList)
            {
                if (!_objectsToCleanDictionary.TryGetValue(interactable, out bool isClean)) continue;
                
                if (!isClean)
                    return interactable;
            }

            return null;
        }

        private void RemoveCustomerFromRoom()
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

        private void ResetObjectsToCleanDictionary()
        {
            foreach (Interactable interactable in _roomObjectList)
                _objectsToCleanDictionary[interactable] = false;
        }

        private void CopyItemsFromListToDictionary()
        {
            foreach (Interactable interactable in _roomObjectList)
                _objectsToCleanDictionary.Add(interactable, false);
        }

        private void SetItemAsCleaned(Interactable interactable)
            => _objectsToCleanDictionary[interactable] = true;
        
        private void CustomerLeftRoom_Event(object sender, EventArgs e)
        {
            RemoveCustomerFromRoom();
            ResetObjectsToCleanDictionary();
            RoomBecameAvailableToCleanStaticEvent.CallRoomBecameAvailableToCleanEvent(this);
        }

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
