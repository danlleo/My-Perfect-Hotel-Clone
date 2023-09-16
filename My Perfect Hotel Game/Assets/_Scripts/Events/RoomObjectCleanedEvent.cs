using System;
using InteractableObject;
using Interfaces;
using UnityEngine;

namespace Events
{
    public class RoomObjectCleanedEvent : MonoBehaviour, IEvent<RoomObjectCleanedEventArgs>
    {
        public event EventHandler<RoomObjectCleanedEventArgs> Event;
    
        public void Call(object sender, RoomObjectCleanedEventArgs eventArgs)
        {
            Event?.Invoke(this, eventArgs);
        }
    }

    public class RoomObjectCleanedEventArgs : EventArgs
    {
        public Interactable CleanedObject;

        public RoomObjectCleanedEventArgs(Interactable cleanedObject)
        {
            CleanedObject = cleanedObject;
        }
    }
}