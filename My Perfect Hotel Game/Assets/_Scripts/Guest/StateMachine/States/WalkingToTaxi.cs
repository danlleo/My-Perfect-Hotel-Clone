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
            _endPosition = Ctx.CurrentGuest.GetTaxiPosition();
        }
        
        public override void ExitState() { }
        
        public override void UpdateState()
        {
            Ctx.CurrentGuest.Movement.MoveTo(_endPosition);
        }
        
        public override void CheckSwitchStates()
        {
            if (Vector3.Distance(Ctx.CurrentGuest.transform.position, _endPosition) <= _stopMovingThreshold)
            {            
                Ctx.CurrentGuest.DestroyGuest();
            }
        }
    }
}