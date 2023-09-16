<<<<<<< Updated upstream
using Events;
=======
using InteractableObject;
>>>>>>> Stashed changes
using UnityEngine;

namespace Entities.Employees.Maid.States
{
    public class MovingState : MaidState
    {
        private readonly float _stopMovingThreshold = .35f;

<<<<<<< Updated upstream
        private MaidStateManager _maidStateManager;
=======
>>>>>>> Stashed changes
        private Vector3 _endPosition;

        public override void EnterState(MaidStateManager maidStateManager)
        {
<<<<<<< Updated upstream
            _maidStateManager = maidStateManager;
            
            var uncleanObject = maidStateManager.CurrentMaid.Room.TryGetUncleanObject();
            var uncleanObjectPosition = uncleanObject.transform.position;
            
            Debug.Log(uncleanObject.name);
            
=======
            Interactable uncleanObject = maidStateManager.CurrentMaid.Room.GetUncleanObject();
            Vector3 uncleanObjectPosition = uncleanObject.transform.position;

>>>>>>> Stashed changes
            maidStateManager.CurrentMaid.SetObjectToClean(uncleanObject);
            _endPosition = new Vector3(uncleanObjectPosition.x, 0f, uncleanObjectPosition.z);
            
            maidStateManager.CurrentMaid.Room.ObjectCleanedEvent.Event += ObjectCleanedEventOnEvent;
            maidStateManager.CurrentMaid.Movement.MoveTo(_endPosition);
        }

        public override void UpdateState(MaidStateManager maidStateManager)
        {
<<<<<<< Updated upstream
            var currentPosition = maidStateManager.CurrentMaid.transform.position;
            
            if (Vector3.Distance(currentPosition, _endPosition) <= _stopMovingThreshold)
            {
                LeaveState(maidStateManager);
            }
=======
            Vector3 currentPosition = maidStateManager.CurrentMaid.transform.position;
            maidStateManager.CurrentMaid.Movement.MoveTo(_endPosition);

            if (Vector3.Distance(currentPosition, _endPosition) <= _stopMovingThreshold) LeaveState(maidStateManager);
>>>>>>> Stashed changes
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
