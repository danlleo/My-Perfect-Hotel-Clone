using UnityEngine;

namespace Entities.Employees.Maid.States
{
    public class MovingToIdlePointState : MaidState
    {
        private readonly float _stopMovingThreshold = .35f;
        
        private MaidStateManager _maidStateManager;
        private Vector3 _endPosition;
    
        public override void EnterState(MaidStateManager maidStateManager)
        {
            Debug.Log("Entered MovingToIdlePointState");
            
            _maidStateManager = maidStateManager;
            _endPosition = maidStateManager.CurrentMaid.GetIdlePoint().position;
            
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
            maidStateManager.SwitchState(maidStateManager.AwaitingState);
        }
    }
}
