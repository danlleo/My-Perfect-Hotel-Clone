using UnityEngine;

namespace Guest.States
{
    public class ReturningToTaxiState : GuestState
    {
        private readonly float _stopMovingThreshold = .125f;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            // ...
        }

        public override void UpdateState(GuestStateManager guestStateManager)
        {
            var currentPosition = guestStateManager.CurrentGuest.transform.position;
            var endPosition = guestStateManager.CurrentGuest.GetTaxiPosition();
            var direction = endPosition - currentPosition;
            
            guestStateManager.CurrentGuest.Movement.MoveTo(direction);

            if (Vector3.Distance(guestStateManager.CurrentGuest.transform.position, endPosition) <= _stopMovingThreshold)
            {
                LeaveState(guestStateManager);
            }
        }

        public override void LeaveState(GuestStateManager guestStateManager)
        {
            guestStateManager.CurrentGuest.DestroyGuest();
        }
    }
}