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
            if (_timer >= Ctx.CurrentGuest.GetTimeItTakesToGuestToSleepInSeconds())
            {
                // Add sleeping animation
                SwitchState(Factory.WalkingToTaxi());
            }
        }

        public override void ExitState()
        {
            // guestStateManager.CurrentGuest.Room.SetIsAvailable();
            _timer = 0f;
            Ctx.CurrentGuest.Room.LeftRoomEvent.Call(Ctx.CurrentGuest.Room);
        }
    }
}