using System;
using Areas;
using Interfaces;
using UnityEngine;

namespace Events
{
    [DisallowMultipleComponent] public class CustomerSetRoomEvent : MonoBehaviour, IEvent<CustomerSetRoomEventArgs>
    {
        public event EventHandler<CustomerSetRoomEventArgs> Event;

        public void Call(object sender, CustomerSetRoomEventArgs setRoom) => Event?.Invoke(sender, setRoom);
    }

    public class CustomerSetRoomEventArgs : EventArgs
    {
        public Room SetRoom;

        public CustomerSetRoomEventArgs(Room room)
        {
            SetRoom = room;
        }
    }
}
