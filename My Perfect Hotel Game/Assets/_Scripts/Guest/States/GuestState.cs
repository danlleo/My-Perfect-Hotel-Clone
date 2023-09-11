namespace Guest.States
{
    public abstract class GuestState
    {
        public abstract void EnterState(GuestStateManager guestStateManager);

        public abstract void UpdateState(GuestStateManager guestStateManager);
    }
}
