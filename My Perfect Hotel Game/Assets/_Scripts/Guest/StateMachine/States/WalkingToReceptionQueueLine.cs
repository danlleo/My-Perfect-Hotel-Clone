using System;
using UnityEngine;

namespace Guest.StateMachine.States
{
    /// <summary>
    /// A lot of stuff is hardcoded for now, just for testing purposes
    /// </summary>
    public class WalkingToReceptionQueueLine : GuestState
    {
        public WalkingToReceptionQueueLine(GuestStateManager currentContext, GuestStateFactory guestStateFactory) : base(currentContext, guestStateFactory) { }
        
        private readonly float _stopMovingThreshold = .2f;
        private GuestStateManager _guestStateManager;
        private Vector3 _endPosition;

        public override void EnterState()
        {
            Ctx.CurrentGuest.GuestReceptionQueueLinePositionChangedEvent.Event += GuestReceptionQueueLinePositionChangedEvent;
            
            _endPosition = Ctx.CurrentGuest.GetPositionInLine();
        }

        public override void ExitState()
        {
            Ctx.CurrentGuest.GuestReceptionQueueLinePositionChangedEvent.Event -= GuestReceptionQueueLinePositionChangedEvent;
        }
        
        public override void UpdateState()
        {
            Ctx.CurrentGuest.Movement.MoveTo(_endPosition);
            
            CheckSwitchStates();
        }
        
        public override void CheckSwitchStates()
        {
            if (Vector3.Distance(Ctx.CurrentGuest.transform.position, _endPosition) <= _stopMovingThreshold)
            {
                SwitchState(Factory.Waiting());
            }
        }
        
        private void GuestReceptionQueueLinePositionChangedEvent(object sender, EventArgs e)
        {
            _endPosition = Ctx.CurrentGuest.GetPositionInLine();
        }
    }
}
