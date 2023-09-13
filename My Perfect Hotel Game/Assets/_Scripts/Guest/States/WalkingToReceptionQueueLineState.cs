using UnityEngine;

namespace Guest.States
{
    /// <summary>
    /// A lot of stuff is hardcoded for now, just for testing purposes
    /// </summary>
    public class WalkingToReceptionQueueLineState : GuestState
    {
        private float _stopMovingThreshold = .125f;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            Debug.Log("Hello");
        }

        public override void UpdateState(GuestStateManager guestStateManager)
        {
            var currentPosition = guestStateManager.CurrentGuest.transform.position;
            var endPosition = guestStateManager.CurrentGuest.GetPositionInLine();
            var direction = endPosition - currentPosition;
            
            guestStateManager.CurrentGuest.Movement.HandleMovement(direction);

            if (Vector3.Distance(currentPosition, endPosition) <= _stopMovingThreshold)
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
