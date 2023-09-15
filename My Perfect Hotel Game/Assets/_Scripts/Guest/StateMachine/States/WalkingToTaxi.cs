using UnityEngine;

namespace Guest.StateMachine.States
{
    public class WalkingToTaxi : GuestState
    {
        public WalkingToTaxi(GuestStateManager currentContext, GuestStateFactory guestStateFactory) : base(currentContext, guestStateFactory) { }
        
        private readonly float _stopMovingThreshold = .2f;
        private Vector3 _endPosition;

        public override void EnterState()
        {
            _endPosition = CurrentContext.Guest.GetTaxiPosition();
        }
        
        public override void ExitState() { }
        
        public override void UpdateState()
        {
            CurrentContext.Guest.Movement.MoveTo(_endPosition);
        }
        
        public override void CheckSwitchStates()
        {
            if (Vector3.Distance(CurrentContext.Guest.transform.position, _endPosition) <= _stopMovingThreshold)
            {            
                CurrentContext.Guest.DestroyGuest();
            }
        }
    }
}