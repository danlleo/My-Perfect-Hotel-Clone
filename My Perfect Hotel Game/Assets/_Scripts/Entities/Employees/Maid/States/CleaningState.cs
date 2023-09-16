using System;
using Events;
using Room;
using UnityEngine;

namespace Entities.Employees.Maid.States
{
    public class CleaningState : MaidState
    {
        private MaidStateManager _maidStateManager;

        public override void EnterState(MaidStateManager maidStateManager)
        {
            _maidStateManager = maidStateManager;
            maidStateManager.CurrentMaid.MaidRemovedFromRoomEvent.Event += MaidRemovedFromRoom_Event;
            _maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event += ObjectCleaned_Event;
        }

        public override void UpdateState(MaidStateManager maidStateManager)
        {
            if (!maidStateManager.CurrentMaid.ObjectToClean.TryInteractWithCallback(out Action onComplete)) return;

            onComplete?.Invoke();

            if (RoomManager.Instance.TryGetUncleanRoom(out Room.Room uncleanRoom))
            {
                maidStateManager.CurrentMaid.SetRoomForCleaning(uncleanRoom);
                maidStateManager.SwitchState(maidStateManager.MovingState);
                return;
            }
<<<<<<< Updated upstream
            
            Debug.Log("I love Rem");
            
=======

>>>>>>> Stashed changes
            LeaveState(maidStateManager);
        }

        public override void LeaveState(MaidStateManager maidStateManager)
        {
            maidStateManager.CurrentMaid.MaidRemovedFromRoomEvent.Event -= MaidRemovedFromRoom_Event;
            maidStateManager.SwitchState(maidStateManager.AwaitingState);
        }

        private void MaidRemovedFromRoom_Event(object sender, EventArgs e)
        {
            LeaveState(_maidStateManager);
        }

        private void ObjectCleaned_Event(object sender, RoomObjectCleanedEventArgs roomObjectCleanedEventArgs)
        {
            _maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event -= ObjectCleaned_Event;
            
            if (ReferenceEquals(roomObjectCleanedEventArgs.CleanedObject, _maidStateManager.CurrentMaid.ObjectToClean))
            {
                Debug.Log("Player was faster");
                LeaveState(_maidStateManager);
            }
        }
    }
}
