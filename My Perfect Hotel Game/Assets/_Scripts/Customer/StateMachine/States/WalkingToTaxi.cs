using UnityEngine;

namespace Customer.StateMachine.States
{
    public class WalkingToTaxi : CustomerState
    {
        public WalkingToTaxi(CustomerStateManager currentContext, CustomerStateFactory customerStateFactory) : base(currentContext, customerStateFactory) { }
        
        private readonly float _stopMovingThreshold = .2f;
        private Vector3 _endPosition;

        public override void EnterState()
        {
            _endPosition = CurrentContext.Customer.GetTaxiPosition();
        }
        
        public override void ExitState() { }
        
        public override void UpdateState()
        {
            CurrentContext.Customer.Movement.MoveTo(_endPosition);
        }
        
        public override void CheckSwitchStates()
        {
            if (Vector3.Distance(CurrentContext.Customer.transform.position, _endPosition) <= _stopMovingThreshold)
            {            
                CurrentContext.Customer.DestroyGuest();
            }
        }
    }
}