using StaticEvents.Room;

namespace Maid.States
{
    public class AwaitingState : MaidState
    {
        private MaidStateManager _maidStateManager;
        
        public override void EnterState(MaidStateManager maidStateManager)
        {
            RoomBecameAvailableToCleanStaticEvent.OnRoomBecameAvailableToClean += RoomBecameAvailableToClean_StaticEvent;

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
        
        private void RoomBecameAvailableToClean_StaticEvent(RoomBecameAvailableToCleanStaticEventArgs roomBecameAvailableToCleanStaticEventArgs)
        {
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