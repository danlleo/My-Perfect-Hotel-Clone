using System;
using UnityEngine;

namespace Events
{
    public class GuestReceptionQueueLinePositionChangedEvent : MonoBehaviour
    {
        public event Action OnGuestReceptionQueueLinePositionChanged;

        public void CallGuestReceptionQueueLinePositionChanged()
            => OnGuestReceptionQueueLinePositionChanged?.Invoke();
    }
}
