using Events;
using InteractableObject;
using Room;
using UnityEngine;

namespace Entities.Employees.Maid.States
{
    public class MovingToUncleanObjectState : MaidState
    {
        private readonly float _stopMovingThreshold = .35f;
        
        private MaidStateManager _maidStateManager;
        private Vector3 _endPosition;

        public override void EnterState(MaidStateManager maidStateManager)
        {
            _maidStateManager = maidStateManager;

            if (!maidStateManager.CurrentMaid.Room.TryGetUncleanObject(out Interactable uncleanObject))
                return;
            
            Vector3 uncleanObjectPosition = uncleanObject.transform.position;
            
            maidStateManager.CurrentMaid.SetObjectToClean(uncleanObject);
            _endPosition = new Vector3(uncleanObjectPosition.x, 0f, uncleanObjectPosition.z);
            
            maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event += ObjectCleaned_Event;
            maidStateManager.CurrentMaid.Movement.MoveTo(_endPosition);
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
            maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event -= ObjectCleaned_Event;
            maidStateManager.SwitchState(maidStateManager.CleaningState);
        }

        #region Events

        private void ObjectCleaned_Event(object sender, RoomObjectCleanedEventArgs roomObjectCleanedEventArgs)
        {
            if (!ReferenceEquals(roomObjectCleanedEventArgs.CleanedObject, _maidStateManager.CurrentMaid.ObjectToClean)) 
                return;
            
            if (!_maidStateManager.CurrentMaid.Room.TryGetUncleanObject(out Interactable uncleanObject))
            {
                if (RoomManager.Instance.TryGetUncleanRoom(out Room.Room uncleanRoom))
                {
                    if (_maidStateManager.CurrentMaid.HasOccupiedRoom())
                        return;

                    if (uncleanRoom.HasMaidOccupied())
                        return;

                    _maidStateManager.CurrentMaid.RemoveObjectToClean();
                    _maidStateManager.CurrentMaid.SetRoomForCleaning(uncleanRoom);
                    _maidStateManager.CurrentMaid.Room.OccupyRoomWithMaid(_maidStateManager.CurrentMaid);
                    _maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event -= ObjectCleaned_Event;
                    _maidStateManager.SwitchState(_maidStateManager.MovingToUncleanObjectState);
                    
                    return;
                }
                
                // If we didn't find unclean room
                _maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event -= ObjectCleaned_Event;
                _maidStateManager.SwitchState(_maidStateManager.MovingToIdlePointState);                
            }
            
            // If another unclean object in this room was found, perform actions
            _maidStateManager.CurrentMaid.SetObjectToClean(uncleanObject);
            LeaveState(_maidStateManager);
            _maidStateManager.SwitchState(_maidStateManager.MovingToUncleanObjectState);
        }

        #endregion
    }
}
