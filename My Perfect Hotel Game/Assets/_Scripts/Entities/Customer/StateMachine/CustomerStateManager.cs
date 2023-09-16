using UnityEngine;

namespace Entities.Customer.StateMachine
{
    [DisallowMultipleComponent] [RequireComponent(typeof(Customer))]
    public class CustomerStateManager : MonoBehaviour
    {
        // state variables
        private CustomerStateFactory _states;
        public Customer Customer { get; private set; }
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
