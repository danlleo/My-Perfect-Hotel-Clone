using System;
using Room;

namespace Maid.States
{
    public class CleaningState : MaidState
    {
        private MaidStateManager _maidStateManager;
        
        public override void EnterState(MaidStateManager maidStateManager)
        {
            _maidStateManager = maidStateManager;
            maidStateManager.CurrentMaid.MaidRemovedFromRoomEvent.Event += MaidRemovedFromRoomEventOnEvent;
        }

        public override void UpdateState(MaidStateManager maidStateManager)
        {
            if (!maidStateManager.CurrentMaid.ObjectToClean.TryInteractWithCallback(out Action onComplete)) return;
            
            onComplete?.Invoke();
                
            if (RoomManager.Instance.TryGetUncleanRoom(out Room.Room uncleanRoom))
            {
                maidStateManager.CurrentMaid.SetRoomForCleaning(uncleanRoom);
                maidStateManager.SwitchState(maidStateManager.MovingState);
            }
            
            LeaveState(maidStateManager);
        }

        public override void LeaveState(MaidStateManager maidStateManager)
        {
            maidStateManager.CurrentMaid.MaidRemovedFromRoomEvent.Event -= MaidRemovedFromRoomEventOnEvent;
            maidStateManager.SwitchState(maidStateManager.AwaitingState);
        }

        private void MaidRemovedFromRoomEventOnEvent(object sender, EventArgs e)
        {
            LeaveState(_maidStateManager);
        }
    }
}