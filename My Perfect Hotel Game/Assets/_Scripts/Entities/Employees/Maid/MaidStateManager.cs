using Entities.Employees.Maid.States;
using UnityEngine;

namespace Entities.Employees.Maid
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Maid))]
    public class MaidStateManager : MonoBehaviour
    {
        public Maid CurrentMaid { get; private set; }
        
        public AwaitingState AwaitingState = new();
        public CleaningState CleaningState = new();
        public MovingToUncleanObjectState MovingToUncleanObjectState = new();
        public MovingToIdlePointState MovingToIdlePointState = new();

        private MaidState _currentState;
        
        private void Awake()
        {
            CurrentMaid = GetComponent<Maid>();
        }

        private void Start()
        {
            SetDefaultState(AwaitingState);
            _currentState.EnterState(this);
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }

        public void SwitchState(MaidState targetState)
        {
            _currentState = targetState;
            _currentState.EnterState(this);
        }

        private void SetDefaultState(MaidState targetState) 
            => _currentState = targetState;
    }
}