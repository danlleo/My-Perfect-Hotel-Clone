using System;
using UnityEngine;

namespace Events
{
    [DisallowMultipleComponent]
    public class GuestSetRoomEvent : MonoBehaviour
    {
        public event Action<GuestSetRoomEventArgs> OnGuestSetRoom;

        public void CallGuestSetRoomEvent(Room.Room setRoom)
            => OnGuestSetRoom?.Invoke(new GuestSetRoomEventArgs(setRoom));
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
