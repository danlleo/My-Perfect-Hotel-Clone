using Events;
using QueueLines.ReceptionQueueLine;
using UnityEngine;

namespace Guest
{
    [RequireComponent(typeof(GuestSetRoomEvent))]
    [RequireComponent(typeof(GuestAppointedEvent))]
    [RequireComponent(typeof(GuestReceptionQueueLinePositionChangedEvent))]
    [RequireComponent(typeof(Movement))]
    [DisallowMultipleComponent]
    public class Guest : MonoBehaviour
    {
        [HideInInspector] public GuestSetRoomEvent GuestSetRoomEvent;
        [HideInInspector] public GuestAppointedEvent GuestAppointedEvent;

        [HideInInspector]
        public GuestReceptionQueueLinePositionChangedEvent GuestReceptionQueueLinePositionChangedEvent;
        
        public bool HasReachedLinePosition { get; private set; }
        
        public Movement Movement { get; private set; }
        public Room.Room Room { get; private set; }
        
        [SerializeField] private float _timeItTakesToGuestToSleepInSeconds;
        
        // Will use it later
        private ReceptionQueueLine _receptionQueueLine;

        private Vector3 _positionInLine;
        private Vector3 _taxiPosition;
        
        private void Awake()
        {
            GuestSetRoomEvent = GetComponent<GuestSetRoomEvent>();
            GuestAppointedEvent = GetComponent<GuestAppointedEvent>();
            GuestReceptionQueueLinePositionChangedEvent = GetComponent<GuestReceptionQueueLinePositionChangedEvent>();
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
            GuestSetRoomEvent.Call(this, new GuestSetRoomEventArgs(room: room));
        }

        public void SetPositionInLine(Vector3 positionInLine)
            => _positionInLine = positionInLine;
        
        public Vector3 GetPositionInLine()
            => _positionInLine;
        
        public Vector3 GetTaxiPosition()
            => _taxiPosition;
        
        public void SetHasReachedLinePosition(bool hasReachedLinePosition)
            => HasReachedLinePosition = hasReachedLinePosition;

        public float GetTimeItTakesToGuestToSleepInSeconds()
            => _timeItTakesToGuestToSleepInSeconds;

        public void DestroyGuest()
            => Destroy(gameObject);
    }
}
