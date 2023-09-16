using Entities.Employees.Maid.States;
using UnityEngine;

namespace Entities.Employees.Maid
{
    [DisallowMultipleComponent] [RequireComponent(typeof(Maid))]
    public class MaidStateManager : MonoBehaviour
    {
<<<<<<< Updated upstream
        public Maid CurrentMaid { get; private set; }
        
=======
        private MaidState _currentState;

>>>>>>> Stashed changes
        public AwaitingState AwaitingState = new();
        public CleaningState CleaningState = new();
<<<<<<< Updated upstream
        
        private MaidState _currentState;
=======
        public MovingState MovingState = new();
        public Maid CurrentMaid { get; private set; }
>>>>>>> Stashed changes

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

        private void SetDefaultState(MaidState targetState) => _currentState = targetState;
    }
}
