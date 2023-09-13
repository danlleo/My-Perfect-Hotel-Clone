using System;
using Interfaces;
using UnityEngine;

namespace Events
{
    public class GuestAppointedEvent : MonoBehaviour, IEvent
    {
        public event EventHandler Event;

        public void Call(object sender)
            => Event?.Invoke(sender, EventArgs.Empty);
    }
}
