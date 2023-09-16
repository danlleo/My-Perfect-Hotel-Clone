using System;
using System.Collections.Generic;
using Entities.Customer;
using Events;
using Room;
using StaticEvents.Reception;
using UnityEngine;
using UnityEngine.Serialization;

namespace QueueLines.ReceptionQueueLine
{
    [RequireComponent(typeof(CustomerSpawnedEvent))]
    [DisallowMultipleComponent]
    public class ReceptionQueueLine : MonoBehaviour
    {
        [HideInInspector] public CustomerSpawnedEvent CustomerSpawnedEvent;
        
        [SerializeField] private int _maxGuestsLimitInQueueLine = 5;
        [SerializeField] private float _distanceBetweenGuestsInLine = 1.15f;
        
        private int _currentGuestsCountInLine;

        private Queue<Customer> _guestsQueue = new();

        private void Awake()
        {
            CustomerSpawnedEvent = GetComponent<CustomerSpawnedEvent>();
        }

        private void OnEnable()
        {
            ReceptionInteractStaticEvent.OnReceptionInteracted += ReceptionInteractStaticEvent_OnOnReceptionInteracted;
            CustomerSpawnedEvent.Event += CustomerSpawnedEventOnOnCustomerSpawned;
        }

        private void OnDisable()
        {
            ReceptionInteractStaticEvent.OnReceptionInteracted -= ReceptionInteractStaticEvent_OnOnReceptionInteracted;
            CustomerSpawnedEvent.Event -= CustomerSpawnedEventOnOnCustomerSpawned;
        }

        public bool IsLineFull()
            => _currentGuestsCountInLine == _maxGuestsLimitInQueueLine;
        
        public Vector3 GetWorldPositionToStayInLine()
        {
            if (_currentGuestsCountInLine is 0)
                return transform.position;

            return transform.position - transform.forward * (_distanceBetweenGuestsInLine * _currentGuestsCountInLine);
        }

        private Vector3 GetWorldPositionToStayInLine(int index)
        {
            if (_currentGuestsCountInLine is 0)
                return transform.position;

            return transform.position - transform.forward * (_distanceBetweenGuestsInLine * index);
        }
        
        private void AddGuestToLine(Customer customer)
        {
            if (_currentGuestsCountInLine == _maxGuestsLimitInQueueLine)
            {
                Debug.LogWarning("Cannot add any more guests to the line because it's full already");
                return;
            }
            
            _guestsQueue.Enqueue(customer);
            IncreaseCurrentGuestsCountInLine();
        }

        private void IncreaseCurrentGuestsCountInLine()
            => _currentGuestsCountInLine++;

        private void DecreaseCurrentGuestsCountInLine()
        {
            if (_currentGuestsCountInLine <= 0)
                throw new ArgumentException("Current guests count in line cannot be less or equal than zero");

            _currentGuestsCountInLine--;
        }
        
        private Customer DequeueNearestStandingToReceptionGuest()
            => _guestsQueue.Dequeue();

        private void UpdatePositionInLineToAllGuests()
        {
            var index = 0;

            foreach (var guest in _guestsQueue)
            {
                guest.SetPositionInLine(GetWorldPositionToStayInLine(index));
                index++;
            }
        }
        
        private void NotifyGuestsInLineAboutPositionChange()
        {
            foreach (var guest in _guestsQueue)
                guest.CustomerReceptionQueueLinePositionChangedEvent.Call(this);
        }
        
        private void ReceptionInteractStaticEvent_OnOnReceptionInteracted(ReceptionInteractStaticEventArgs receptionInteractStaticEventArgs)
        {
            if (_guestsQueue.Count == 0)
                return;

            if (!RoomManager.Instance.HasAvailableRoom())
                return;
            
            if (!_guestsQueue.Peek().HasReachedLinePosition)
                return;
            
            receptionInteractStaticEventArgs.InteractedReception.AppointGuestToRoom(DequeueNearestStandingToReceptionGuest());
            receptionInteractStaticEventArgs.OnSuccess?.Invoke();
            
            DecreaseCurrentGuestsCountInLine();
            UpdatePositionInLineToAllGuests();
            NotifyGuestsInLineAboutPositionChange();
        }
        
        private void CustomerSpawnedEventOnOnCustomerSpawned(object sender, CustomerSpawnedEventArgs customerSpawnedEventArgs)
        {
            AddGuestToLine(customerSpawnedEventArgs.Customer);
        }
    }
}
