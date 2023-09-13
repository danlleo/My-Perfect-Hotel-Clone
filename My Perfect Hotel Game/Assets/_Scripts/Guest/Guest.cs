using Events;
using QueueLines.ReceptionQueueLine;
using UnityEngine;

namespace Guest
{
    [RequireComponent(typeof(GuestSetRoomEvent))]
    [RequireComponent(typeof(Movement))]
    public class Guest : MonoBehaviour
    {
        [HideInInspector] public GuestSetRoomEvent GuestSetRoomEvent;
        
        public Movement Movement { get; private set; }
        
        private Room.Room _room;
        private ReceptionQueueLine _receptionQueueLine;

        private Vector3 _positionInLine;

        private void Awake()
        {
            GuestSetRoomEvent = GetComponent<GuestSetRoomEvent>();
            Movement = GetComponent<Movement>();
        }

        public void Initialize(ReceptionQueueLine receptionQueueLine, Vector3 positionInLine)
        {
            _receptionQueueLine = receptionQueueLine;
            _positionInLine = positionInLine;
        }

        public void SetRoom(Room.Room room)
        {
            _room = room;
            GuestSetRoomEvent.CallGuestSetRoomEvent(_room);
        }

        public Vector3 GetPositionInLine()
            => _positionInLine;
    }
}
