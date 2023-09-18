using System;
using Events;
using InteractableObject;
using Room;
using StaticEvents.Room;
using UnityEngine;

namespace Entities.Employees.Maid.States
{
    public class CleaningState : MaidState
    {
        private MaidStateManager _maidStateManager;

        public override void EnterState(MaidStateManager maidStateManager)
        {
            Debug.Log("Entered CleaningState");

            _maidStateManager = maidStateManager;
            _maidStateManager.CurrentMaid.MaidRemovedFromRoomEvent.Event += MaidRemovedFromRoom_Event;
            _maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event += ObjectCleaned_Event;
            RoomCleanedStaticEvent.OnRoomCleaned += RoomCleaned_StaticEvent;
        }

        public override void UpdateState(MaidStateManager maidStateManager)
        {
            if (!_maidStateManager.CurrentMaid.HasObjectToClean())
                return;
            
            if (!maidStateManager.CurrentMaid.ObjectToClean.TryInteractWithCallback(out Action onComplete)) return;

            onComplete?.Invoke();
        }

        public override void LeaveState(MaidStateManager maidStateManager)
        {
            maidStateManager.CurrentMaid.MaidRemovedFromRoomEvent.Event -= MaidRemovedFromRoom_Event;
            RoomCleanedStaticEvent.OnRoomCleaned -= RoomCleaned_StaticEvent;
        }

        private void MaidRemovedFromRoom_Event(object sender, EventArgs e)
        {
            LeaveState(_maidStateManager);
        }

        #region Events

        private void ObjectCleaned_Event(object sender, RoomObjectCleanedEventArgs roomObjectCleanedEventArgs)
        {
            if (!ReferenceEquals(roomObjectCleanedEventArgs.CleanedObject, _maidStateManager.CurrentMaid.ObjectToClean))
                return;

            if (!_maidStateManager.CurrentMaid.Room.TryGetUncleanObject(out Interactable uncleanObject)) 
                return;
            
            // If another unclean object in this room was found, perform actions
            _maidStateManager.CurrentMaid.SetObjectToClean(uncleanObject);
            LeaveState(_maidStateManager);
            _maidStateManager.SwitchState(_maidStateManager.MovingState);
        }

        private void RoomCleaned_StaticEvent(RoomCleanedStaticEventArgs roomCleanedStaticEventArgs)
        {
            if (!ReferenceEquals(_maidStateManager.CurrentMaid.Room, roomCleanedStaticEventArgs.Room)) return;
            
            _maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event -= ObjectCleaned_Event;
            _maidStateManager.CurrentMaid.RemoveRoomForCleaning();

            if (RoomManager.Instance.TryGetUncleanRoom(out Room.Room uncleanRoom))
            {
                if (_maidStateManager.CurrentMaid.HasOccupiedRoom())
                    return;

                if (uncleanRoom.HasMaidOccupied())
                    return;

                _maidStateManager.CurrentMaid.RemoveObjectToClean();
                _maidStateManager.CurrentMaid.SetRoomForCleaning(uncleanRoom);
                _maidStateManager.CurrentMaid.Room.OccupyRoomWithMaid(_maidStateManager.CurrentMaid);
                
                LeaveState(_maidStateManager);
                _maidStateManager.SwitchState(_maidStateManager.MovingState);
                
                return;
            }
            
            LeaveState(_maidStateManager);
            _maidStateManager.SwitchState(_maidStateManager.AwaitingState);
        }

        #endregion
    }
}