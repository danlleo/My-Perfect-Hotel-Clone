using Events;
using QueueLines.ReceptionQueueLine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Customer
{
    [RequireComponent(typeof(CustomerSetRoomEvent))]
    [RequireComponent(typeof(CustomerAppointedEvent))]
    [RequireComponent(typeof(CustomerReceptionQueueLinePositionChangedEvent))]
    [RequireComponent(typeof(Movement))]
    [DisallowMultipleComponent]
    public class Customer : MonoBehaviour
    {
        [FormerlySerializedAs("GuestSetRoomEvent")] [HideInInspector] public CustomerSetRoomEvent CustomerSetRoomEvent;
        [FormerlySerializedAs("GuestAppointedEvent")] [HideInInspector] public CustomerAppointedEvent CustomerAppointedEvent;

        [FormerlySerializedAs("GuestReceptionQueueLinePositionChangedEvent")] [HideInInspector]
        public CustomerReceptionQueueLinePositionChangedEvent CustomerReceptionQueueLinePositionChangedEvent;
        
        public bool HasReachedLinePosition { get; private set; }
        
        public Movement Movement { get; private set; }
        public Room.Room Room { get; private set; }
        
        [SerializeField] private float _timeItTakesToGuestToSleepInSeconds;
        
        // TODO: Use it later
        private ReceptionQueueLine _receptionQueueLine;

        private Vector3 _positionInLine;
        private Vector3 _taxiPosition;
        
        private void Awake()
        {
            CustomerSetRoomEvent = GetComponent<CustomerSetRoomEvent>();
            CustomerAppointedEvent = GetComponent<CustomerAppointedEvent>();
            CustomerReceptionQueueLinePositionChangedEvent = GetComponent<CustomerReceptionQueueLinePositionChangedEvent>();
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
            CustomerSetRoomEvent.Call(this, new CustomerSetRoomEventArgs(room: room));
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
