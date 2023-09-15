namespace Guest.StateMachine
{
    public abstract class GuestState
    {
        public GuestState(GuestStateManager currentContext, GuestStateFactory guestStateFactory)
        {
            CurrentContext = currentContext;
            Factory = guestStateFactory;
        }
        
        protected readonly GuestStateManager CurrentContext;
        protected readonly GuestStateFactory Factory;
        
        public abstract void EnterState();
        
        public abstract void ExitState();
            
        public abstract void UpdateState();
        
        public abstract void CheckSwitchStates();

        protected void SwitchState(GuestState newState)
        {
            ExitState();
            
            newState.EnterState();

            CurrentContext.CurrentState = newState;
        }
    }
}
