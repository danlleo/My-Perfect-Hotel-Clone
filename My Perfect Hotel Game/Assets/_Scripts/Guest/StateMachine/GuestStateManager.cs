using UnityEngine;

namespace Guest.StateMachine
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Guest))]
    public class GuestStateManager : MonoBehaviour
    {
        public Guest Guest { get; private set; }

        // state variables
        private GuestStateFactory _states;
        public GuestState CurrentState { get; set; }
        
        
        private void Start()
        {
            Guest = GetComponent<Guest>();
            
            _states = new GuestStateFactory(this);
            
            CurrentState = _states.WalkingToReceptionQueueLine();
            CurrentState.EnterState();
        }
        
        private void Update() => CurrentState.UpdateState();
    }
}
