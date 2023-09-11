using UnityEngine;

namespace Guest.States
{
    [DisallowMultipleComponent]
    public class GuestStateManager : MonoBehaviour
    {
        private GuestState _currentState;

        private WalkingTowardsReceptionLineState _walkingTowardsReceptionLineState = new();

        private void Start()
        {
            _currentState = _walkingTowardsReceptionLineState;
            
            _currentState.EnterState(this);
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }

        public void SwitchState(GuestState targetState)
        {
            _currentState = targetState;
            _currentState.EnterState(this);
        }
    }
}
