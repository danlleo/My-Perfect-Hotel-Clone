using System;
using UnityEngine;

namespace Entities.Customer.StateMachine.States
{
    /// <summary>
    ///     A lot of stuff is hardcoded for now, just for testing purposes
    /// </summary>
    public class WalkingToReceptionQueueLine : CustomerState
    {
        private readonly float _stopMovingThreshold = .2f;
        private CustomerStateManager _customerStateManager;
        private Vector3 _endPosition;
        public WalkingToReceptionQueueLine(CustomerStateManager currentContext, CustomerStateFactory customerStateFactory) :
            base(currentContext, customerStateFactory) { }

        public override void EnterState()
        {
            CurrentContext.Customer.CustomerReceptionQueueLinePositionChangedEvent.Event +=
                CustomerReceptionQueueLinePositionChangedEvent;

            _endPosition = CurrentContext.Customer.GetPositionInLine();
        }

        public override void ExitState()
        {
            CurrentContext.Customer.CustomerReceptionQueueLinePositionChangedEvent.Event -=
                CustomerReceptionQueueLinePositionChangedEvent;
        }

        public override void UpdateState()
        {
            CurrentContext.Customer.Movement.MoveTo(_endPosition);

            CheckSwitchStates();
        }

        public override void CheckSwitchStates()
        {
            if (Vector3.Distance(CurrentContext.Customer.transform.position, _endPosition) <= _stopMovingThreshold)
                SwitchState(Factory.Waiting());
        }

        private void CustomerReceptionQueueLinePositionChangedEvent(object sender, EventArgs e)
        {
            _endPosition = CurrentContext.Customer.GetPositionInLine();
        }
    }
}
