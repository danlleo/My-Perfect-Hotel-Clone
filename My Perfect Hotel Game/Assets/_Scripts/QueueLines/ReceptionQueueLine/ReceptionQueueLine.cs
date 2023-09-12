using System.Collections.Generic;
using StaticEvents.Reception;
using UnityEngine;

namespace QueueLines.ReceptionQueueLine
{
    public class ReceptionQueueLine : MonoBehaviour
    {
        [SerializeField] private int _maxGuestsLimitInQueueLine = 5;

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

        private void ReceptionInteractStaticEvent_OnOnReceptionInteracted(ReceptionInteractStaticEventArgs receptionInteractStaticEventArgs)
        {
            if (_guestsQueue.Count > 0)
                receptionInteractStaticEventArgs.InteractedReception.AppointGuestToRoom(GetNearestStandingToReceptionGuest());
        }

        private Guest.Guest GetNearestStandingToReceptionGuest()
            => _guestsQueue.Dequeue();
    }
}
