using System;
using Interfaces;
using UnityEngine;

namespace Events
{
    [DisallowMultipleComponent]
    public class GuestSpawnedEvent : MonoBehaviour, IEvent<GuestSpawnedEventArgs>
    {
        public event EventHandler<GuestSpawnedEventArgs> Event;

        public void Call(object sender, GuestSpawnedEventArgs guest)
            => Event?.Invoke(sender, guest);
    }

    public class GuestSpawnedEventArgs : EventArgs
    {
        public readonly Guest.Guest Guest;

        public GuestSpawnedEventArgs(Guest.Guest guest)
        {
            Guest = guest;
        }
    }
}