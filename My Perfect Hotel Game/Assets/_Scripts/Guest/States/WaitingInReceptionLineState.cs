using UnityEngine;

namespace Guest.States
{
    public class WaitingInReceptionLineState : GuestState
    {
        private readonly float _stopMovingThreshold = .125f;
        private bool _isAppointed;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            guestStateManager.CurrentGuest.SetIsWaitingInLine(true);
            guestStateManager.CurrentGuest.GuestAppointedEvent.OnGuestAppointed += GuestAppointedEvent_OnGuestAppointed;
        }
        
        public override void UpdateState(GuestStateManager guestStateManager)
        {
            if (!_isAppointed)
                return;
            
            var currentPosition = guestStateManager.CurrentGuest.transform.position;
            var endPosition = guestStateManager.CurrentGuest.Room.GetBedPosition();
            var direction = endPosition - currentPosition;
            
            guestStateManager.CurrentGuest.Movement.MoveTo(direction);

            if (Vector3.Distance(guestStateManager.CurrentGuest.transform.position, endPosition) <= _stopMovingThreshold)
            {
                LeaveState(guestStateManager);
            }
        }

        public override void LeaveState(GuestStateManager guestStateManager)
        {
            guestStateManager.CurrentGuest.GuestAppointedEvent.OnGuestAppointed -= GuestAppointedEvent_OnGuestAppointed;
        }
        
        private void GuestAppointedEvent_OnGuestAppointed()
        {
            _isAppointed = true;
        }
    }
}
