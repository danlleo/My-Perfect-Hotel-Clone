using Room;
using UnityEngine;

namespace Maid.States
{
    public class AwaitingState : MaidState
    {
        public override void EnterState(MaidStateManager maidStateManager)
        {
            Debug.Log("Entered Awaiting State");
        }

        public override void UpdateState(MaidStateManager maidStateManager)
        {
            /*
            if (RoomManager.Instance.TryGetUncleanRoom(out Room.Room room))
            {
                Debug.Log("Found unclean room");
                
                room.OccupyRoomWithMaid(maidStateManager.CurrentMaid);
                maidStateManager.CurrentMaid.SetRoomForCleaning(room);
                LeaveState(maidStateManager);
            }
            */
        }

        public override void LeaveState(MaidStateManager maidStateManager)
        {
            maidStateManager.SwitchState(maidStateManager.MovingState);
        }
    }
}