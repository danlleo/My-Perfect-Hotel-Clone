namespace Guest.States
{
    public class WaitingInReceptionLineState : GuestState
    {
        private GuestStateManager _guestStateManager;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            guestStateManager.CurrentGuest.SetHasReachedLinePosition(true);
            guestStateManager.CurrentGuest.GuestAppointedEvent.OnGuestAppointed += GuestAppointedEvent_OnGuestAppointed;
            guestStateManager.CurrentGuest.GuestReceptionQueueLinePositionChangedEvent.OnGuestReceptionQueueLinePositionChanged += GuestReceptionQueueLinePositionChangedEvent_OnGuestReceptionQueueLinePositionChanged;
            
            _guestStateManager = guestStateManager;
        }

        public override void UpdateState(GuestStateManager guestStateManager)
        {
            // ...
        }

        public override void LeaveState(GuestStateManager guestStateManager)
        {
            guestStateManager.CurrentGuest.SetHasReachedLinePosition(false);
            guestStateManager.CurrentGuest.GuestAppointedEvent.OnGuestAppointed -= GuestAppointedEvent_OnGuestAppointed;
            guestStateManager.CurrentGuest.GuestReceptionQueueLinePositionChangedEvent.OnGuestReceptionQueueLinePositionChanged -= GuestReceptionQueueLinePositionChangedEvent_OnGuestReceptionQueueLinePositionChanged;
            guestStateManager.SwitchState(guestStateManager.WalkingToRoomBed);
        }
        
        private void GuestAppointedEvent_OnGuestAppointed()
        {
            LeaveState(_guestStateManager);
        }
        
        private void GuestReceptionQueueLinePositionChangedEvent_OnGuestReceptionQueueLinePositionChanged()
        {
            _guestStateManager.CurrentGuest.SetHasReachedLinePosition(false);
            _guestStateManager.SwitchState(_guestStateManager.WalkingToReceptionQueueLine);
        }
    }
}
