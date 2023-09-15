namespace Guest.StateMachine
{
    public abstract class GuestState
    {
        public GuestState(GuestStateManager currentContext, GuestStateFactory guestStateFactory)
        {
            Ctx = currentContext;
            Factory = guestStateFactory;
        }
        
        protected readonly GuestStateManager Ctx;
        protected readonly GuestStateFactory Factory;
        
        public abstract void EnterState();
        
        public abstract void ExitState();
        
        public abstract void UpdateState();
        
        public abstract void CheckSwitchStates();

        protected void SwitchState(GuestState newState)
        {
            ExitState();
            
            newState.EnterState();

            Ctx.CurrentState = newState;
        }
    }
}
