using Events;
using UnityEngine;

namespace Guest
{
    [RequireComponent(typeof(GuestSetRoomEvent))]
    [RequireComponent(typeof(Movement))]
    public class Guest : MonoBehaviour
    {
        [HideInInspector] public GuestSetRoomEvent GuestSetRoomEvent;
        
        public Movement Movement { get; private set; }

        // For now add through the inspector, later add through the code
        [SerializeField] private Transform _receptionLineQueueTransform;

        private Room.Room _room;

        private void Awake()
        {
            GuestSetRoomEvent = GetComponent<GuestSetRoomEvent>();
            Movement = GetComponent<Movement>();
        }

        public void SetRoom(Room.Room room)
        {
            _room = room;
            GuestSetRoomEvent.CallGuestSetRoomEvent(_room);
        }
        
        // ...
    }
}
