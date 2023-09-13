using System;
using Interfaces;
using UnityEngine;

namespace Events
{
    [DisallowMultipleComponent]
    public class GuestSetRoomEvent : MonoBehaviour, IEvent<GuestSetRoomEventArgs>
    {
        public event EventHandler<GuestSetRoomEventArgs> Event;

        public void Call(object sender, GuestSetRoomEventArgs setRoom)
            => Event?.Invoke(sender, setRoom);
    }

    public class GuestSetRoomEventArgs : EventArgs
    {
        public Room.Room SetRoom;

        public GuestSetRoomEventArgs(Room.Room room)
        {
            SetRoom = room;
        }
    }
}
