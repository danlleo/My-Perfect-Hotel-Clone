using Events;
using UnityEngine;

namespace Entities.Employees.Maid.States
{
    public class MovingState : MaidState
    {
        private readonly float _stopMovingThreshold = .35f;
        private MaidStateManager _maidStateManager;
        private Vector3 _endPosition;

        public override void EnterState(MaidStateManager maidStateManager)
        {
            _maidStateManager = maidStateManager;
            
            var uncleanObject = maidStateManager.CurrentMaid.Room.TryGetUncleanObject();
            var uncleanObjectPosition = uncleanObject.transform.position;
            
            Debug.Log(uncleanObject.name);

            maidStateManager.CurrentMaid.SetObjectToClean(uncleanObject);
            _endPosition = new Vector3(uncleanObjectPosition.x, 0f, uncleanObjectPosition.z);
            
            maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event += ObjectCleanedEventOnEvent;
            maidStateManager.CurrentMaid.Movement.MoveTo(_endPosition);
        }

        public override void UpdateState(MaidStateManager maidStateManager)
        {
            var currentPosition = maidStateManager.CurrentMaid.transform.position;
            
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

        private void ObjectCleanedEventOnEvent(object sender, RoomObjectCleanedEventArgs e)
        {
            if (ReferenceEquals(e.CleanedObject, _maidStateManager.CurrentMaid.ObjectToClean))
            {
                _maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event -= ObjectCleanedEventOnEvent;
                _maidStateManager.SwitchState(_maidStateManager.AwaitingState);
            }
        }
    }
}
