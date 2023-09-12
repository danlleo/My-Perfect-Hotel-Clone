using Events;
using UnityEngine;

namespace Guest
{
    [RequireComponent(typeof(GuestSetRoomEvent))]
    public class Guest : MonoBehaviour
    {
        [HideInInspector] public GuestSetRoomEvent GuestSetRoomEvent;
        
        private Room.Room _room;

        private void Awake()
        {
            GuestSetRoomEvent = GetComponent<GuestSetRoomEvent>();
        }

        public void SetRoom(Room.Room room)
        {
            _room = room;
            GuestSetRoomEvent.CallGuestSetRoomEvent(_room);
        }
        
        // ...
    }
}
