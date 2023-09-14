using UnityEngine;

namespace Guest.States
{
    public class ReturningToTaxiState : GuestState
    {
        private readonly float _stopMovingThreshold = .125f;

        private Vector3 _endPosition;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            _endPosition = guestStateManager.CurrentGuest.GetTaxiPosition();
            
            Debug.Log("Privet");
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
            guestStateManager.CurrentGuest.DestroyGuest();
        }
    }
}