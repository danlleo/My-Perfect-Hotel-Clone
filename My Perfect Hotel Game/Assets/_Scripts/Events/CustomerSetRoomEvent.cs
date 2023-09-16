using System;
using Interfaces;
using UnityEngine;

namespace Events
{
    [DisallowMultipleComponent]
    public class CustomerSetRoomEvent : MonoBehaviour, IEvent<CustomerSetRoomEventArgs>
    {
        public event EventHandler<CustomerSetRoomEventArgs> Event;

        public void Call(object sender, CustomerSetRoomEventArgs setRoom)
            => Event?.Invoke(sender, setRoom);
    }

    public class CustomerSetRoomEventArgs : EventArgs
    {
        public Room.Room SetRoom;

        public CustomerSetRoomEventArgs(Room.Room room)
        {
            SetRoom = room;
        }
    }
}
