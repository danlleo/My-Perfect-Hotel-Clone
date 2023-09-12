using UnityEngine;

namespace Guest.States
{
    /// <summary>
    /// A lot of stuff is hardcoded for now, just for testing purposes
    /// </summary>
    public class WalkingToReceptionQueueLineState : GuestState
    {
        private Vector3 _endPosition = new Vector3(0f, 0f, -4.25f);
        private float _threshold = .125f;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            Debug.Log("Hello");
        }

        public override void UpdateState(GuestStateManager guestStateManager)
        {
            var direction = _endPosition - guestStateManager.CurrentGuest.transform.position;
            
            guestStateManager.CurrentGuest.Movement.HandleMovement(direction);

            if (Vector3.Distance(guestStateManager.CurrentGuest.transform.position, _endPosition) <= _threshold)
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
