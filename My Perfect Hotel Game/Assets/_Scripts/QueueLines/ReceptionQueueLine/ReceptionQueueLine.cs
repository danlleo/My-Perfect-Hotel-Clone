using System;
using System.Collections.Generic;
using Events;
using Room;
using StaticEvents.Reception;
using UnityEngine;

namespace QueueLines.ReceptionQueueLine
{
    [RequireComponent(typeof(GuestSpawnedEvent))]
    [DisallowMultipleComponent]
    public class ReceptionQueueLine : MonoBehaviour
    {
        [HideInInspector] public GuestSpawnedEvent GuestSpawnedEvent;

        [SerializeField] private Reception.Reception _reception;
        
        [SerializeField] private int _maxGuestsLimitInQueueLine = 5;
        [SerializeField] private float _distanceBetweenGuestsInLine = 1.15f;
        
        private int _currentGuestsCountInLine;

        private Queue<Guest.Guest> _guestsQueue = new();

        private void Awake()
        {
            GuestSpawnedEvent = GetComponent<GuestSpawnedEvent>();
        }

        private void OnEnable()
        {
            ReceptionInteractStaticEvent.OnReceptionInteracted += ReceptionInteractStaticEvent_OnOnReceptionInteracted;
            GuestSpawnedEvent.OnGuestSpawned += GuestSpawnedEvent_OnOnGuestSpawned;
        }

        private void OnDisable()
        {
            ReceptionInteractStaticEvent.OnReceptionInteracted -= ReceptionInteractStaticEvent_OnOnReceptionInteracted;
            GuestSpawnedEvent.OnGuestSpawned -= GuestSpawnedEvent_OnOnGuestSpawned;
        }

        public bool IsLineFull()
            => _currentGuestsCountInLine == _maxGuestsLimitInQueueLine;
        
        public Vector3 GetWorldPositionToStayInLine()
        {
            if (_currentGuestsCountInLine is 0)
                return transform.position;

            return transform.position - transform.forward * (_distanceBetweenGuestsInLine * _currentGuestsCountInLine);
        }
        
        public void RemoveGuestFromLine()
            => _guestsQueue.Dequeue();
        
        private void AddGuestToLine(Guest.Guest guest)
        {
            if (_currentGuestsCountInLine == _maxGuestsLimitInQueueLine)
            {
                Debug.LogWarning("Cannot add any more guests to the line because it's full already");
                return;
            }
            
            _guestsQueue.Enqueue(guest);
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
        
        private Guest.Guest GetNearestStandingToReceptionGuest()
            => _guestsQueue.Dequeue();
        
        private void ReceptionInteractStaticEvent_OnOnReceptionInteracted(ReceptionInteractStaticEventArgs receptionInteractStaticEventArgs)
        {
            if (_guestsQueue.Count == 0)
                return;

            if (!RoomManager.Instance.HasAvailableRoom())
                return;
            
            if (!_guestsQueue.Peek().IsWaitingInLine)
                return;
            
            receptionInteractStaticEventArgs.InteractedReception.AppointGuestToRoom(GetNearestStandingToReceptionGuest());
        }
        
        private void GuestSpawnedEvent_OnOnGuestSpawned(GuestSpawnedEventArgs guestSpawnedEventArgs)
        {
            AddGuestToLine(guestSpawnedEventArgs.Guest);
        }
    }
}
