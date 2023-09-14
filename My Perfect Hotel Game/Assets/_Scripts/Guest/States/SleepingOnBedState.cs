using UnityEngine;

namespace Guest.States
{
    public class SleepingOnBedState : GuestState
    {
        private float _sleepingTimeInSeconds;
        private float _timer;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            _sleepingTimeInSeconds = guestStateManager.CurrentGuest.GetTimeItTakesToGuestToSleepInSeconds();
        }

        public override void UpdateState(GuestStateManager guestStateManager)
        {
            _timer += Time.deltaTime;

            if (_timer >= _sleepingTimeInSeconds)
            {
                // Add sleeping animation
                
                LeaveState(guestStateManager);
            }
        }

        public override void LeaveState(GuestStateManager guestStateManager)
        {
            // guestStateManager.CurrentGuest.Room.SetIsAvailable();
            guestStateManager.CurrentGuest.Room.LeftRoomEvent.Call(guestStateManager.CurrentGuest.Room);
            guestStateManager.SwitchState(guestStateManager.ReturningToTaxiState);
        }
    }
}