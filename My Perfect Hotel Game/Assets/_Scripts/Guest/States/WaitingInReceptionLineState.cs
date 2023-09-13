namespace Guest.States
{
    public class WaitingInReceptionLineState : GuestState
    {
        private GuestStateManager _guestStateManager;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            guestStateManager.CurrentGuest.SetIsWaitingInLine(true);
            guestStateManager.CurrentGuest.GuestAppointedEvent.OnGuestAppointed += GuestAppointedEvent_OnGuestAppointed;

            _guestStateManager = guestStateManager;
        }
        
        public override void UpdateState(GuestStateManager guestStateManager)
        {
            // ...
        }

        public override void LeaveState(GuestStateManager guestStateManager)
        {
            guestStateManager.CurrentGuest.SetIsWaitingInLine(false);
            guestStateManager.CurrentGuest.GuestAppointedEvent.OnGuestAppointed -= GuestAppointedEvent_OnGuestAppointed;
            guestStateManager.SwitchState(guestStateManager.WalkingToRoomBed);
        }
        
        private void GuestAppointedEvent_OnGuestAppointed()
        {
            LeaveState(_guestStateManager);
        }
    }
}
