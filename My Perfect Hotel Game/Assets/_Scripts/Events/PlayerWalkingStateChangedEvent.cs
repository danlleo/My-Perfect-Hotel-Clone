using System;
using Interfaces;
using UnityEngine;

namespace Events
{
    [DisallowMultipleComponent]
    public class PlayerWalkingStateChangedEvent : MonoBehaviour, IEvent<PlayerWalkingStateChangedEventArgs>
    {
        public event EventHandler<PlayerWalkingStateChangedEventArgs> Event;

        public void Call(object sender, PlayerWalkingStateChangedEventArgs eventArgs)
        {
            Event?.Invoke(sender, eventArgs);
        }
    }

    public class PlayerWalkingStateChangedEventArgs : EventArgs
    {
        public readonly bool IsWalking;

        public PlayerWalkingStateChangedEventArgs(bool isWalking)
        {
            IsWalking = isWalking;
        }
    }
}
