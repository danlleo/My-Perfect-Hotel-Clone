using System;
using Interfaces;
using UnityEngine;

namespace Events
{
    [DisallowMultipleComponent]
    public class GuestLeftRoomEvent : MonoBehaviour, IEvent
    {
        public event EventHandler Event;

        public void Call(object sender)
            => Event?.Invoke(sender, EventArgs.Empty);
    }
}
