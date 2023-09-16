using System;
using Entities.Customer;
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
        public readonly Customer Customer;

        public CustomerSpawnedEventArgs(Customer customer)
        {
            Customer = customer;
        }
    }
}