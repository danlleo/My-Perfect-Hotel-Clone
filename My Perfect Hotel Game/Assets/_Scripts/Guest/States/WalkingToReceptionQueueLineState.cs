using System;
using UnityEngine;

namespace Guest.States
{
    /// <summary>
    /// A lot of stuff is hardcoded for now, just for testing purposes
    /// </summary>
    public class WalkingToReceptionQueueLineState : GuestState
    {
        private readonly float _stopMovingThreshold = .2f;

        private GuestStateManager _guestStateManager;
        private Vector3 _endPosition;
        
        public override void EnterState(GuestStateManager guestStateManager)
        {
            guestStateManager.CurrentGuest.GuestReceptionQueueLinePositionChangedEvent.Event += GuestReceptionQueueLinePositionChangedEventEvent;
            
            _endPosition = guestStateManager.CurrentGuest.GetPositionInLine();
            _guestStateManager = guestStateManager;
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
            guestStateManager.SwitchState(guestStateManager.WaitingInReceptionLineState);
        }

        private void GuestReceptionQueueLinePositionChangedEventEvent(object sender, EventArgs e)
        {
            _endPosition = _guestStateManager.CurrentGuest.GetPositionInLine();
        }
    }
}
