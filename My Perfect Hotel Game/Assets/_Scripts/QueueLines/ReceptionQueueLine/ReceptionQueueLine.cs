using System.Collections.Generic;
using StaticEvents.Reception;
using UnityEngine;

namespace QueueLines.ReceptionQueueLine
{
    public class ReceptionQueueLine : MonoBehaviour
    {
        [SerializeField] private int _maxGuestsLimitInQueueLine = 5;
        [SerializeField] private float _distanceBetweenGuestsInLine = 1.15f;
        
        private int _currentGuestsCountInReceptionQueueLine;

        private Queue<Guest.Guest> _guestsQueue = new();

        private void OnEnable()
        {
            ReceptionInteractStaticEvent.OnReceptionInteracted += ReceptionInteractStaticEvent_OnOnReceptionInteracted;
        }

        private void OnDisable()
        {
            ReceptionInteractStaticEvent.OnReceptionInteracted -= ReceptionInteractStaticEvent_OnOnReceptionInteracted;
        }

        public Vector3 GetWorldPositionToStayInLine()
        {
            if (_currentGuestsCountInReceptionQueueLine == 0 || _currentGuestsCountInReceptionQueueLine == 1)
                return transform.position;

            return Vector3.back * _distanceBetweenGuestsInLine * _currentGuestsCountInReceptionQueueLine;
        }

        public void AddGuestToReceptionQueueLine(Guest.Guest guest)
        {
            if (_currentGuestsCountInReceptionQueueLine == _maxGuestsLimitInQueueLine)
            {
                Debug.LogWarning("Cannot add any more guests to the line because it's full already");
                return;
            }
            
            _guestsQueue.Enqueue(guest);
        }
        
        private Guest.Guest GetNearestStandingToReceptionGuest()
            => _guestsQueue.Dequeue();
        
        private void ReceptionInteractStaticEvent_OnOnReceptionInteracted(ReceptionInteractStaticEventArgs receptionInteractStaticEventArgs)
        {
            if (_guestsQueue.Count > 0)
                receptionInteractStaticEventArgs.InteractedReception.AppointGuestToRoom(GetNearestStandingToReceptionGuest());
        }
    }
}
