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

            CurrentContext.Customer.CustomerAppointedEvent.Event += CustomerAppointed_Event;

            CurrentContext.Customer.CustomerReceptionQueueLinePositionChangedEvent.Event +=
                CustomerReceptionQueueLinePositionChanged_Event;
        }

        public override void ExitState()
        {
            CurrentContext.Customer.SetHasReachedLinePosition(false);

            CurrentContext.Customer.CustomerAppointedEvent.Event -= CustomerAppointed_Event;

            CurrentContext.Customer.CustomerReceptionQueueLinePositionChangedEvent.Event -=
                CustomerReceptionQueueLinePositionChanged_Event;
        }

        public override void UpdateState() { }

        public override void CheckSwitchStates() { }

        #region Events

        private void CustomerAppointed_Event(object sender, EventArgs _)
        {
            SwitchState(Factory.WalkingToRoomBed());
        }

        private void CustomerReceptionQueueLinePositionChanged_Event(object sender, EventArgs _)
        {
            CurrentContext.Customer.SetHasReachedLinePosition(false);

            SwitchState(Factory.WalkingToReceptionQueueLine());
        }

        #endregion
    }
}
