using UnityEngine;

namespace Guest.States
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Guest))]
    public class GuestStateManager : MonoBehaviour
    {
        public Guest CurrentGuest { get; private set; }
        
        private GuestState _currentState;

        public WalkingToReceptionQueueLineState WalkingToReceptionQueueLine = new();
        public WaitingInReceptionLineState WaitingInReceptionLine = new();
        public BookingRoomState BookingRoom = new();
        public WalkingToRoomBedState WalkingToRoomBed = new();

        private void Awake()
        {
            CurrentGuest = GetComponent<Guest>();
        }

        private void Start()
        {
            _currentState = WalkingToReceptionQueueLine;
            _currentState.EnterState(this);
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }

        public void SwitchState(GuestState targetState)
        {
            _currentState = targetState;
            _currentState.EnterState(this);
        }
    }
}
