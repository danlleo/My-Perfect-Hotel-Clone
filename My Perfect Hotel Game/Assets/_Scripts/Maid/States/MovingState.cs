using UnityEngine;

namespace Maid.States
{
    public class MovingState : MaidState
    {
        public override void EnterState(MaidStateManager maidStateManager)
        {
            Debug.Log("Found unclean room");
        }

        public override void UpdateState(MaidStateManager maidStateManager)
        {
            // ...
        }

        public override void LeaveState(MaidStateManager maidStateManager)
        {
            throw new System.NotImplementedException();
        }
    }
}