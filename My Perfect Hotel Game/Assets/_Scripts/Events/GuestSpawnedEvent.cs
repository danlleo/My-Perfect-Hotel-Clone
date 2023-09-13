using System;
using UnityEngine;

namespace Events
{
    [DisallowMultipleComponent]
    public class GuestSpawnedEvent : MonoBehaviour
    {
        public event Action<GuestSpawnedEventArgs> OnGuestSpawned;

        public void CallGuestSpawnedEvent(Guest.Guest guest)
            => OnGuestSpawned?.Invoke(new GuestSpawnedEventArgs(guest));
    }

    public class GuestSpawnedEventArgs : EventArgs
    {
        public Guest.Guest Guest;

        public GuestSpawnedEventArgs(Guest.Guest guest)
        {
            Guest = guest;
        }
    }
}