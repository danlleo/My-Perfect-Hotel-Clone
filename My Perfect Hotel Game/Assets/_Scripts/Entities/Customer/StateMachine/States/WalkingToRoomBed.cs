using UnityEngine;

namespace Entities.Customer.StateMachine.States
{
    public class WalkingToRoomBed : CustomerState
    {
        private readonly float _stopMovingThreshold = .25f;
        private Vector3 _endPosition;
        public WalkingToRoomBed(CustomerStateManager currentContext, CustomerStateFactory customerStateFactory) : base(
            currentContext, customerStateFactory) { }

        public override void EnterState()
        {
            _endPosition = CurrentContext.Customer.Room.GetBedPosition();
        }

        public override void ExitState() { }

        public override void UpdateState()
        {
            CurrentContext.Customer.Movement.MoveTo(_endPosition);

            CheckSwitchStates();
        }

        public override void CheckSwitchStates()
        {
            if (Vector3.Distance(CurrentContext.Customer.transform.position, _endPosition) <= _stopMovingThreshold)
                SwitchState(Factory.Sleeping());
        }
    }
}
