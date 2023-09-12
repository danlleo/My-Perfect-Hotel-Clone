using Events;
using UnityEngine;

namespace Guest
{
    [RequireComponent(typeof(GuestSetRoomEvent))]
    public class Guest : MonoBehaviour
    {
        private GuestSetRoomEvent _guestSetRoomEvent;
        private Room.Room _room;

        private void Awake()
        {
            _guestSetRoomEvent = GetComponent<GuestSetRoomEvent>();
        }

        public void SetRoom(Room.Room room)
        {
            _room = room;
            _guestSetRoomEvent.CallGuestSetRoomEvent(_room);
        }
        
        // ...
    }
}
