using UnityEngine;

namespace Guest.States
{
    public class WalkingTowardsReceptionLineState : GuestState
    {
        public override void EnterState(GuestStateManager guestStateManager)
        {
            Debug.Log("Entered State");
        }

        public override void UpdateState(GuestStateManager guestStateManager)
        {
            Debug.Log("Updated State");
        }
    }
}
