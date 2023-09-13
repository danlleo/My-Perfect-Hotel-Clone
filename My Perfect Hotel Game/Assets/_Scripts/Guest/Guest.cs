using Events;
using QueueLines.ReceptionQueueLine;
using UnityEngine;

namespace Guest
{
    [RequireComponent(typeof(GuestSetRoomEvent))]
    [RequireComponent(typeof(GuestAppointedEvent))]
    [RequireComponent(typeof(Movement))]
    public class Guest : MonoBehaviour
    {
        [HideInInspector] public GuestSetRoomEvent GuestSetRoomEvent;
        [HideInInspector] public GuestAppointedEvent GuestAppointedEvent;
        
        public bool IsWaitingInLine { get; private set; }
        
        public Movement Movement { get; private set; }
        public Room.Room Room { get; private set; }
        
        private ReceptionQueueLine _receptionQueueLine;

        private Vector3 _positionInLine;
        
        private void Awake()
        {
            GuestSetRoomEvent = GetComponent<GuestSetRoomEvent>();
            GuestAppointedEvent = GetComponent<GuestAppointedEvent>();
            Movement = GetComponent<Movement>();
        }

        public void Initialize(ReceptionQueueLine receptionQueueLine, Vector3 positionInLine)
        {
            _receptionQueueLine = receptionQueueLine;
            _positionInLine = positionInLine;
        }

        public void SetRoom(Room.Room room)
        {
            Room = room;
            GuestSetRoomEvent.CallGuestSetRoomEvent(Room);
        }

        public Vector3 GetPositionInLine()
            => _positionInLine;

        public void SetIsWaitingInLine(bool isWaitingInLine)
            => IsWaitingInLine = isWaitingInLine;
    }
}
