using Guest.StateMachine;
using UnityEngine;

namespace Guest
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Guest))]
    public class GuestStateManager : MonoBehaviour
    {
        public Guest CurrentGuest { get; private set; }

        // state variables
        private GuestStateFactory _states;
        public GuestState CurrentState { get; set; }
        
        
        private void Start()
        {
            CurrentGuest = GetComponent<Guest>();
            
            _states = new GuestStateFactory(this);
            
            CurrentState = _states.WalkingToReceptionQueueLine();
            CurrentState.EnterState();
        }
        
        private void Update() => CurrentState.UpdateState();
    }
}
