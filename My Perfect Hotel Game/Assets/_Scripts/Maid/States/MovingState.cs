using UnityEngine;

namespace Maid.States
{
    public class MovingState : MaidState
    {
        private readonly float _stopMovingThreshold = .25f;
        
        private Vector3 _endPosition;
        
        public override void EnterState(MaidStateManager maidStateManager)
        {
            _endPosition = maidStateManager.CurrentMaid.Room.GetUncleanObject().transform.position;
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
            Debug.Log("Farted");
        }
    }
}