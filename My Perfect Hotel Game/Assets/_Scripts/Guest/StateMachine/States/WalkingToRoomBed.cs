using UnityEngine;

namespace Guest.StateMachine.States
{
    public class WalkingToRoomBed : GuestState
    {
        public WalkingToRoomBed(GuestStateManager currentContext, GuestStateFactory guestStateFactory) : base(currentContext, guestStateFactory) { }
        
        private readonly float _stopMovingThreshold = .25f;
        private Vector3 _endPosition;

        public override void EnterState()
        {
            _endPosition = CurrentContext.Guest.Room.GetBedPosition();
        }
        
        public override void ExitState() { }
        
        public override void UpdateState()
        {
            CurrentContext.Guest.Movement.MoveTo(_endPosition);
            
            CheckSwitchStates();
        }
        
        public override void CheckSwitchStates()
        {
            if (Vector3.Distance(CurrentContext.Guest.transform.position, _endPosition) <= _stopMovingThreshold)
            {
                SwitchState(Factory.Sleeping());
            }
        }
    }
}
