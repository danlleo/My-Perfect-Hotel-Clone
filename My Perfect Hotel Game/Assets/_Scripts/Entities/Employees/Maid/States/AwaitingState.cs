using StaticEvents.Room;
using UnityEngine;

namespace Entities.Employees.Maid.States
{
    public class AwaitingState : MaidState
    {
        private MaidStateManager _maidStateManager;

        public override void EnterState(MaidStateManager maidStateManager)
        {
            Debug.Log("Entered AwaitingState");
            
            RoomBecameAvailableToCleanStaticEvent.OnRoomBecameAvailableToClean += RoomBecameAvailableToClean_StaticEvent;
            
            maidStateManager.CurrentMaid.Movement.ClearDestination();
            _maidStateManager = maidStateManager;
        }

        public override void UpdateState(MaidStateManager maidStateManager)
        {
            // ...
        }

        public override void LeaveState(MaidStateManager maidStateManager)
        {
            RoomBecameAvailableToCleanStaticEvent.OnRoomBecameAvailableToClean -= RoomBecameAvailableToClean_StaticEvent;
            
            maidStateManager.SwitchState(maidStateManager.MovingState);
        }

        private void RoomBecameAvailableToClean_StaticEvent(
            RoomBecameAvailableToCleanStaticEventArgs roomBecameAvailableToCleanStaticEventArgs)
        {
            Debug.Log("Has Maid occupied the room: " + _maidStateManager.CurrentMaid.HasOccupiedRoom());
            
            if (_maidStateManager.CurrentMaid.HasOccupiedRoom())
                return;

            if (roomBecameAvailableToCleanStaticEventArgs.Room.HasMaidOccupied())
                return;

            _maidStateManager.CurrentMaid.SetRoomForCleaning(roomBecameAvailableToCleanStaticEventArgs.Room);
            _maidStateManager.CurrentMaid.Room.OccupyRoomWithMaid(_maidStateManager.CurrentMaid);

            LeaveState(_maidStateManager);
        }
    }
}
