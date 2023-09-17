using System;
using Events;
using QueueLines.ReceptionQueueLine;
using UnityEngine;
using Utilities;

namespace Entities.Customer
{
    [RequireComponent(typeof(CustomerSetRoomEvent))]
    [RequireComponent(typeof(CustomerAppointedEvent))]
    [RequireComponent(typeof(CustomerReceptionQueueLinePositionChangedEvent))]
    [RequireComponent(typeof(Movement))]
    [DisallowMultipleComponent]
    public class Customer : Entity
    {
        [HideInInspector] public CustomerSetRoomEvent CustomerSetRoomEvent;
        [HideInInspector] public CustomerAppointedEvent CustomerAppointedEvent;
        [HideInInspector] public CustomerReceptionQueueLinePositionChangedEvent CustomerReceptionQueueLinePositionChangedEvent;

        [SerializeField] [Min(0)] private float _timeItTakesToGuestToSleepInSeconds;

        private Vector3 _positionInLine;

        // TODO: Use it later
        private ReceptionQueueLine _receptionQueueLine;
        private Vector3 _taxiPosition;

        public bool HasReachedLinePosition { get; private set; }

        public Movement Movement { get; private set; }
        public Room.Room Room { get; private set; }

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
            CustomerSetRoomEvent.Call(this, new CustomerSetRoomEventArgs(room));
        }

        public void SetPositionInLine(Vector3 positionInLine) => _positionInLine = positionInLine;

        public Vector3 GetPositionInLine() => _positionInLine;

        public Vector3 GetTaxiPosition() => _taxiPosition;

        public void SetHasReachedLinePosition(bool hasReachedLinePosition) => HasReachedLinePosition = hasReachedLinePosition;

        public float GetTimeItTakesToGuestToSleepInSeconds() => _timeItTakesToGuestToSleepInSeconds;

        public void DestroyGuest() => Destroy(gameObject);

        protected override Vector3 GetNextDestination() => throw new NotImplementedException();

        #region Validation
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsPositiveValue(this, nameof(_timeItTakesToGuestToSleepInSeconds),
                _timeItTakesToGuestToSleepInSeconds);
        }
#endif

        #endregion
    }
}
