using System;

namespace Guest.StateMachine.States
{
    public class WaitingInReceptionLine : GuestState
    {
        public WaitingInReceptionLine(GuestStateManager currentContext, GuestStateFactory guestStateFactory) : base(currentContext, guestStateFactory) { }
        
        public override void EnterState()
        {
            Ctx.CurrentGuest.SetHasReachedLinePosition(true);
            
            Ctx.CurrentGuest.GuestAppointedEvent.Event += GuestAppointedEvent;
            Ctx.CurrentGuest.GuestReceptionQueueLinePositionChangedEvent.Event += GuestReceptionQueueLinePositionChangedEvent;
        }
        
        public override void ExitState()
        {
            Ctx.CurrentGuest.SetHasReachedLinePosition(false);
            
            Ctx.CurrentGuest.GuestAppointedEvent.Event -= GuestAppointedEvent;
            Ctx.CurrentGuest.GuestReceptionQueueLinePositionChangedEvent.Event -= GuestReceptionQueueLinePositionChangedEvent;
        }
        
        public override void UpdateState() { }
        
        public override void CheckSwitchStates() { }

        private void GuestAppointedEvent(object sender, EventArgs _)
        {
            SwitchState(Factory.WalkingToRoomBed());
        }
        
        private void GuestReceptionQueueLinePositionChangedEvent(object sender, EventArgs _)
        {
            Ctx.CurrentGuest.SetHasReachedLinePosition(false);
            
            SwitchState(Factory.WalkingToReceptionQueueLine());
        }
    }
}
