using Maid.States;
using UnityEngine;

namespace Maid
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Maid))]
    public class MaidStateManager : MonoBehaviour
    {
        public Maid CurrentMaid { get; private set; }

        private MaidState _currentState;

        public AwaitingState AwaitingState = new();
        public MovingState MovingState = new();
        public CleaningState CleaningState = new();

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