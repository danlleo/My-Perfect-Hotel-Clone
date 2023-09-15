using UnityEngine;

namespace Guest.StateMachine.States
{
    public class SleepingOnBed : GuestState
    {
        public SleepingOnBed(GuestStateManager currentContext, GuestStateFactory guestStateFactory) : base(currentContext, guestStateFactory) { }

        private float _timer;
        
        public override void EnterState() { }

        public override void UpdateState()
        {
            _timer += Time.deltaTime;
            CheckSwitchStates();
        }
        
        public override void CheckSwitchStates()
        {
            if (_timer >= CurrentContext.Guest.GetTimeItTakesToGuestToSleepInSeconds())
            {
                // Add sleeping animation
                SwitchState(Factory.WalkingToTaxi());
            }
        }

        public override void ExitState()
        {
            // guestStateManager.CurrentGuest.Room.SetIsAvailable();
            _timer = 0f;
            CurrentContext.Guest.Room.LeftRoomEvent.Call(CurrentContext.Guest.Room);
        }
    }
}