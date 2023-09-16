using System;

namespace Entities.Customer.StateMachine.States
{
    public class WaitingInReceptionLine : CustomerState
    {
        public WaitingInReceptionLine(CustomerStateManager currentContext, CustomerStateFactory customerStateFactory) : base(
            currentContext, customerStateFactory) { }

        public override void EnterState()
        {
            CurrentContext.Customer.SetHasReachedLinePosition(true);

            CurrentContext.Customer.CustomerAppointedEvent.Event += CustomerAppointedEvent;

            CurrentContext.Customer.CustomerReceptionQueueLinePositionChangedEvent.Event +=
                CustomerReceptionQueueLinePositionChangedEvent;
        }

        public override void ExitState()
        {
            CurrentContext.Customer.SetHasReachedLinePosition(false);

            CurrentContext.Customer.CustomerAppointedEvent.Event -= CustomerAppointedEvent;

            CurrentContext.Customer.CustomerReceptionQueueLinePositionChangedEvent.Event -=
                CustomerReceptionQueueLinePositionChangedEvent;
        }

        public override void UpdateState() { }

        public override void CheckSwitchStates() { }

        private void CustomerAppointedEvent(object sender, EventArgs _)
        {
            SwitchState(Factory.WalkingToRoomBed());
        }

        private void CustomerReceptionQueueLinePositionChangedEvent(object sender, EventArgs _)
        {
            CurrentContext.Customer.SetHasReachedLinePosition(false);

            SwitchState(Factory.WalkingToReceptionQueueLine());
        }
    }
}
