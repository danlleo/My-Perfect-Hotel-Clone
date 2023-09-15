using System;

namespace Guest.StateMachine.States
{
    public class WaitingInReceptionLine : GuestState
    {
        public WaitingInReceptionLine(GuestStateManager currentContext, GuestStateFactory guestStateFactory) : base(currentContext, guestStateFactory) { }
        
        public override void EnterState()
        {
            CurrentContext.Guest.SetHasReachedLinePosition(true);
            
            CurrentContext.Guest.GuestAppointedEvent.Event += GuestAppointedEvent;
            CurrentContext.Guest.GuestReceptionQueueLinePositionChangedEvent.Event += GuestReceptionQueueLinePositionChangedEvent;
        }
        
        public override void ExitState()
        {
            CurrentContext.Guest.SetHasReachedLinePosition(false);
            
            CurrentContext.Guest.GuestAppointedEvent.Event -= GuestAppointedEvent;
            CurrentContext.Guest.GuestReceptionQueueLinePositionChangedEvent.Event -= GuestReceptionQueueLinePositionChangedEvent;
        }
        
        public override void UpdateState() { }
        
        public override void CheckSwitchStates() { }

        private void GuestAppointedEvent(object sender, EventArgs _)
        {
            SwitchState(Factory.WalkingToRoomBed());
        }
        
        private void GuestReceptionQueueLinePositionChangedEvent(object sender, EventArgs _)
        {
            CurrentContext.Guest.SetHasReachedLinePosition(false);
            
            SwitchState(Factory.WalkingToReceptionQueueLine());
        }
    }
}
