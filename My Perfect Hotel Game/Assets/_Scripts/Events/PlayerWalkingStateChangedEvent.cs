using System;
using Interfaces;
using UnityEngine;

namespace Events
{
    public class PlayerWalkingStateChangedEvent : MonoBehaviour, IEvent
    {
        public event EventHandler<bool> Event;

        public void Call(object sender, bool isWalking) => 
            Event?.Invoke(sender, isWalking);
    }
}
