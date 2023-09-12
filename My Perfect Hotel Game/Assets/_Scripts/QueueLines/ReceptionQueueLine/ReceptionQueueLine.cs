using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace QueueLines.ReceptionQueueLine
{
    public class ReceptionQueueLine : Singleton<ReceptionQueueLine>
    {
        [SerializeField] private int _maxGuestsLimitInQueueLine = 5;

        private int _currentGuestsCountInReceptionQueueLine;

        private Queue<Guest.Guest> _guestsQueue = new();

        public Guest.Guest PeakNearestStandingToReceptionGuest()
            => _guestsQueue.Peek();
    }
}
