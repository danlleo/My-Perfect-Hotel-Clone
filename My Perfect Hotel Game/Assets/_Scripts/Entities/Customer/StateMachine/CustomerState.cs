namespace Entities.Customer.StateMachine
{
    public abstract class CustomerState
    {
        protected readonly CustomerStateManager CurrentContext;
        protected readonly CustomerStateFactory Factory;
        public CustomerState(CustomerStateManager currentContext, CustomerStateFactory customerStateFactory)
        {
            CurrentContext = currentContext;
            Factory = customerStateFactory;
        }

        public abstract void EnterState();

        public abstract void ExitState();

        public abstract void UpdateState();

        public abstract void CheckSwitchStates();

        protected void SwitchState(CustomerState newState)
        {
            ExitState();

            newState.EnterState();

            CurrentContext.CurrentState = newState;
        }
    }
}
