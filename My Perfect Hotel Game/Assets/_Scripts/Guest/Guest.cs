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

        [SerializeField] private float _timeItTakesToGuestToSleepInSeconds;
        
        private ReceptionQueueLine _receptionQueueLine;

        private Vector3 _positionInLine;
        private Vector3 _taxiPosition;
        
        private void Awake()
        {
            GuestSetRoomEvent = GetComponent<GuestSetRoomEvent>();
            GuestAppointedEvent = GetComponent<GuestAppointedEvent>();
            Movement = GetComponent<Movement>();
        }

        public void Initialize(ReceptionQueueLine receptionQueueLine, Vector3 positionInLine, Vector3 taxiPosition)
        {
            _receptionQueueLine = receptionQueueLine;
            _positionInLine = positionInLine;
            _taxiPosition = taxiPosition;
        }

        public void SetRoom(Room.Room room)
        {
            Room = room;
            GuestSetRoomEvent.CallGuestSetRoomEvent(Room);
        }

        public Vector3 GetPositionInLine()
            => _positionInLine;
        
        public Vector3 GetTaxiPosition()
            => _taxiPosition;
        
        public void SetIsWaitingInLine(bool isWaitingInLine)
            => IsWaitingInLine = isWaitingInLine;

        public float GetTimeItTakesToGuestToSleepInSeconds()
            => _timeItTakesToGuestToSleepInSeconds;
    }
}
