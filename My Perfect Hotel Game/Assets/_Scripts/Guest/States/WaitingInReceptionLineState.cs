using System;

namespace Guest.States
{
    public class WaitingInReceptionLineState : GuestState
    {
        private GuestStateManager _guestStateManager;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            guestStateManager.CurrentGuest.SetHasReachedLinePosition(true);
            guestStateManager.CurrentGuest.GuestAppointedEvent.Event += GuestAppointedEventEvent;
            guestStateManager.CurrentGuest.GuestReceptionQueueLinePositionChangedEvent.Event += GuestReceptionQueueLinePositionChangedEventEvent;
            
            _guestStateManager = guestStateManager;
        }

        public override void UpdateState(GuestStateManager guestStateManager)
        {
            // ...
        }

        public override void LeaveState(GuestStateManager guestStateManager)
        {
            guestStateManager.CurrentGuest.SetHasReachedLinePosition(false);
            guestStateManager.CurrentGuest.GuestAppointedEvent.Event -= GuestAppointedEventEvent;
            guestStateManager.CurrentGuest.GuestReceptionQueueLinePositionChangedEvent.Event -= GuestReceptionQueueLinePositionChangedEventEvent;
            guestStateManager.SwitchState(guestStateManager.WalkingToRoomBed);
        }
        
        private void GuestAppointedEventEvent(object sender, EventArgs _)
        {
            LeaveState(_guestStateManager);
        }
        
        private void GuestReceptionQueueLinePositionChangedEventEvent(object sender, EventArgs _)
        {
            _guestStateManager.CurrentGuest.SetHasReachedLinePosition(false);
            _guestStateManager.SwitchState(_guestStateManager.WalkingToReceptionQueueLine);
        }
    }
}
