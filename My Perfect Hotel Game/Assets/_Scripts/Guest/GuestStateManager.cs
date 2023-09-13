using Guest.States;
using UnityEngine;

namespace Guest
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Guest))]
    public class GuestStateManager : MonoBehaviour
    {
        public Guest CurrentGuest { get; private set; }
        
        private GuestState _currentState;

        public WalkingToReceptionQueueLineState WalkingToReceptionQueueLine = new();
        public WaitingInReceptionLineState WaitingInReceptionLine = new();
        public WalkingToRoomBedState WalkingToRoomBed = new();
        public SleepingOnBedState SleepingOnBed = new();
        public ReturningToTaxiState ReturningToTaxiState = new();

        private void Awake()
        {
            CurrentGuest = GetComponent<Guest>();
        }

        private void Start()
        {
            SetDefaultState(WalkingToReceptionQueueLine);
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

        private void SetDefaultState(GuestState targetState)
            => _currentState = targetState;
    }
}
