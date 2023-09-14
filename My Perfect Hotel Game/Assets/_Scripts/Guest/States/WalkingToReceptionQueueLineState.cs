using UnityEngine;

namespace Guest.States
{
    /// <summary>
    /// A lot of stuff is hardcoded for now, just for testing purposes
    /// </summary>
    public class WalkingToReceptionQueueLineState : GuestState
    {
        private readonly float _stopMovingThreshold = .2f;
        
        private Vector3 _endPosition;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            _endPosition = guestStateManager.CurrentGuest.GetPositionInLine();
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
            guestStateManager.SwitchState(guestStateManager.WaitingInReceptionLine);
        }
    }
}
