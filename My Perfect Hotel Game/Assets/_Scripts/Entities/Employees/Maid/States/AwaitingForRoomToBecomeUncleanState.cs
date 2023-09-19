using StaticEvents.Room;
using UnityEngine;

namespace Entities.Employees.Maid.States
{
    public class AwaitingForRoomToBecomeUncleanState : MaidState
    {
        private MaidStateManager _maidStateManager;

        public override void EnterState(MaidStateManager maidStateManager)
        {
            Debug.Log("Entered AwaitingForRoomToBecomeUncleanState");
            
            RoomBecameAvailableToCleanStaticEvent.OnRoomBecameAvailableToClean += RoomBecameAvailableToClean_StaticEvent;
            
            maidStateManager.CurrentMaid.Movement.ClearDestination();
            _maidStateManager = maidStateManager;
        }

        public override void UpdateState(MaidStateManager maidStateManager) { }

        public override void LeaveState(MaidStateManager maidStateManager)
        {
            RoomBecameAvailableToCleanStaticEvent.OnRoomBecameAvailableToClean -= RoomBecameAvailableToClean_StaticEvent;
            
            maidStateManager.SwitchState(maidStateManager.MovingToUncleanObjectState);
        }

        #region Events

        private void RoomBecameAvailableToClean_StaticEvent(
            RoomBecameAvailableToCleanStaticEventArgs roomBecameAvailableToCleanStaticEventArgs)
        {
            if (roomBecameAvailableToCleanStaticEventArgs.Room.HasMaidOccupied)
                return;
            
            if (_maidStateManager.CurrentMaid.HasOccupiedRoom())
                return;
            
            _maidStateManager.CurrentMaid.SetRoomForCleaning(roomBecameAvailableToCleanStaticEventArgs.Room);
            _maidStateManager.CurrentMaid.Room.OccupyRoomWithMaid();
            
            LeaveState(_maidStateManager);
        }

        #endregion
    }
}
