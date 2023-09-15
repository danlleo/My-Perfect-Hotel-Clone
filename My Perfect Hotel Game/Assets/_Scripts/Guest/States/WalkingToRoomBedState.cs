using UnityEngine;

namespace Guest.States
{
    public class WalkingToRoomBedState : GuestState
    {
        private readonly float _stopMovingThreshold = .25f;
        
        private Vector3 _endPosition;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            _endPosition = guestStateManager.CurrentGuest.Room.GetBedPosition();
        }

        public override void UpdateState(GuestStateManager guestStateManager)
        {
            var currentPosition = guestStateManager.CurrentGuest.transform.position;
            
            guestStateManager.CurrentGuest.Movement.MoveTo(_endPosition);

            if (Vector3.Distance(currentPosition, _endPosition) <= _stopMovingThreshold)
            {
                LeaveState(guestStateManager);
            }
        }

        public override void LeaveState(GuestStateManager guestStateManager)
        {
            guestStateManager.SwitchState(guestStateManager.SleepingOnBedState);
        }
    }
}
