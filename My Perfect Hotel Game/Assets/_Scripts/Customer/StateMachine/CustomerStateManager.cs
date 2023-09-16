using UnityEngine;

namespace Customer.StateMachine
{
    [DisallowMultipleComponent] 
    [RequireComponent(typeof(Customer))]
    public class CustomerStateManager : MonoBehaviour
    {
        public Customer Customer { get; private set; }

        // state variables
        private CustomerStateFactory _states;
        public CustomerState CurrentState { get; set; }


        private void Start()
        {
            Customer = GetComponent<Customer>();

            _states = new CustomerStateFactory(this);

            CurrentState = _states.WalkingToReceptionQueueLine();
            CurrentState.EnterState();
        }

        private void Update() => CurrentState.UpdateState();
    }
}
