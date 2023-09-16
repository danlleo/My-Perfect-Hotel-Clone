using UnityEngine;

namespace Maid.States
{
    public class MovingState : MaidState
    {
        private readonly float _stopMovingThreshold = .35f;
        
        private Vector3 _endPosition;
        
        public override void EnterState(MaidStateManager maidStateManager)
        {
            var uncleanObject = maidStateManager.CurrentMaid.Room.GetUncleanObject();
            var uncleanObjectPosition = uncleanObject.transform.position;
            
            maidStateManager.CurrentMaid.SetObjectToClean(uncleanObject);
            _endPosition = new Vector3(uncleanObjectPosition.x, maidStateManager.transform.position.y, uncleanObjectPosition.z);
        }

        public override void UpdateState(MaidStateManager maidStateManager)
        {
            var currentPosition = maidStateManager.CurrentMaid.transform.position;
            maidStateManager.CurrentMaid.Movement.MoveTo(_endPosition);
            
            if (Vector3.Distance(currentPosition, _endPosition) <= _stopMovingThreshold)
            {
                LeaveState(maidStateManager);
            }
        }

        public override void LeaveState(MaidStateManager maidStateManager)
        {
            maidStateManager.SwitchState(maidStateManager.CleaningState);
        }
    }
}