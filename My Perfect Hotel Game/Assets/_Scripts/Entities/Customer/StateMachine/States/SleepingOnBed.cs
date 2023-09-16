using UnityEngine;

namespace Entities.Customer.StateMachine.States
{
    public class SleepingOnBed : CustomerState
    {
        private float _timer;
        public SleepingOnBed(CustomerStateManager currentContext, CustomerStateFactory customerStateFactory) : base(
            currentContext, customerStateFactory) { }

        public override void EnterState() { }

        public override void UpdateState()
        {
            _timer += Time.deltaTime;
            CheckSwitchStates();
        }

        public override void CheckSwitchStates()
        {
            if (_timer >= CurrentContext.Customer.GetTimeItTakesToGuestToSleepInSeconds())
                // Add sleeping animation
                SwitchState(Factory.WalkingToTaxi());
        }

        public override void ExitState()
        {
            // guestStateManager.CurrentGuest.Room.SetIsAvailable();
            _timer = 0f;
            CurrentContext.Customer.Room.LeftRoomEvent.Call(CurrentContext.Customer.Room);
        }
    }
}
