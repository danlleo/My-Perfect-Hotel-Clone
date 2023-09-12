using UnityEngine;

namespace Guest.States
{
    public class WaitingInReceptionLineState : GuestState
    {
        public override void EnterState(GuestStateManager guestStateManager)
        {
            Debug.Log("WaitingInReceptionLineState");
        }

        public override void UpdateState(GuestStateManager guestStateManager)
        {
            // ...
        }

        public override void LeaveState(GuestStateManager guestStateManager)
        {
            // ...
        }
    }
}
