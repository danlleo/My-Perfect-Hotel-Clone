using StaticEvents.Room;
using UnityEngine;

namespace Entities.Employees.Maid.States
{
    public class MovingToIdlePointState : MaidState
    {
        private readonly float _stopMovingThreshold = .35f;
        
        private MaidStateManager _maidStateManager;
        private Vector3 _endPosition;
    
        public override void EnterState(MaidStateManager maidStateManager)
        {
            Debug.Log("Entered MovingToIdlePointState");
            
            _maidStateManager = maidStateManager;
            _endPosition = maidStateManager.CurrentMaid.GetIdlePoint().position;
            
            maidStateManager.CurrentMaid.Movement.MoveTo(_endPosition);
            
            RoomBecameAvailableToCleanStaticEvent.OnRoomBecameAvailableToClean += RoomBecameAvailableToCleanStaticEventOnOnRoomBecameAvailableToClean;
        }

        public override void UpdateState(MaidStateManager maidStateManager)
        {
            Vector3 currentPosition = maidStateManager.CurrentMaid.transform.position;
            
            if (Vector3.Distance(currentPosition, _endPosition) <= _stopMovingThreshold)
            {
                LeaveState(maidStateManager);
            }
        }

        public override void LeaveState(MaidStateManager maidStateManager)
        {
            RoomBecameAvailableToCleanStaticEvent.OnRoomBecameAvailableToClean -= RoomBecameAvailableToCleanStaticEventOnOnRoomBecameAvailableToClean;
            
            maidStateManager.SwitchState(maidStateManager.AwaitingState);
        }

        #region Events

        private void RoomBecameAvailableToCleanStaticEventOnOnRoomBecameAvailableToClean(RoomBecameAvailableToCleanStaticEventArgs obj)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
