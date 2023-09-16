using System;
using Interfaces;
using UnityEngine;

namespace Events
{
    [DisallowMultipleComponent]
    public class CustomerSpawnedEvent : MonoBehaviour, IEvent<CustomerSpawnedEventArgs>
    {
        public event EventHandler<CustomerSpawnedEventArgs> Event;

        public void Call(object sender, CustomerSpawnedEventArgs customer)
            => Event?.Invoke(sender, customer);
    }

    public class CustomerSpawnedEventArgs : EventArgs
    {
        public readonly Customer.Customer Customer;

        public CustomerSpawnedEventArgs(Customer.Customer customer)
        {
            Customer = customer;
        }
    }
}