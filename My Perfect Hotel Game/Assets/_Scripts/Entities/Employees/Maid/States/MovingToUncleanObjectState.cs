using Events;
using InteractableObject;
using StaticEvents.Room;
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
            
            maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event += ObjectCleanedEventOnEvent;
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
            maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event -= ObjectCleanedEventOnEvent;
            maidStateManager.SwitchState(maidStateManager.CleaningState);
        }

        #region Events

        private void ObjectCleanedEventOnEvent(object sender, RoomObjectCleanedEventArgs e)
        {
            if (!ReferenceEquals(e.CleanedObject, _maidStateManager.CurrentMaid.ObjectToClean)) return;
            
            _maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event -= ObjectCleanedEventOnEvent;
            _maidStateManager.SwitchState(_maidStateManager.AwaitingState);
        }

        #endregion
    }
}
