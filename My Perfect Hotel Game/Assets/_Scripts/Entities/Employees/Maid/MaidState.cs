namespace Entities.Employees.Maid
{
    public abstract class MaidState
    {
        public abstract void EnterState(MaidStateManager maidStateManager);

        public abstract void UpdateState(MaidStateManager maidStateManager);

        public abstract void LeaveState(MaidStateManager maidStateManager);
    }
}
