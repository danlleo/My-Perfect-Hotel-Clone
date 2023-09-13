using System;
using UnityEngine;

namespace Events
{
    public class GuestAppointedEvent : MonoBehaviour
    {
        public event Action OnGuestAppointed;

        public void CallGuestAppointedEvent()
            => OnGuestAppointed?.Invoke();
    }
}
