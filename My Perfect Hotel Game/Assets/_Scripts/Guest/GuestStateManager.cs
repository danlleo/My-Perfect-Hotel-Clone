using Guest.States;
using UnityEngine;

namespace Guest
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Guest))]
    public class GuestStateManager : MonoBehaviour
    {
        public Guest CurrentGuest { get; private set; }
        
        public WalkingToReceptionQueueLineState WalkingToReceptionQueueLineState = new();
        public WaitingInReceptionLineState WaitingInReceptionLineState = new();
        public WalkingToRoomBedState WalkingToRoomBedState = new();
        public SleepingOnBedState SleepingOnBedState = new();
        public WalkingToTaxiState WalkingToTaxiState = new();

        private GuestState _currentState;
        
        private void Awake()
        {
            CurrentGuest = GetComponent<Guest>();
        }

        private void Start()
        {
            SetDefaultState(WalkingToReceptionQueueLineState);
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
