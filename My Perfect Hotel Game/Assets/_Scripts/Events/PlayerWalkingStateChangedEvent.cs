using System;
using UnityEngine;

namespace Events
{
    public class PlayerWalkingStateChangedEvent : MonoBehaviour
    {
        public event EventHandler<bool> Event;

        public void Call(object sender, bool isWalking) => 
            Event?.Invoke(sender, isWalking);
    }
}
