using UnityEngine;

namespace TransportableObjects
{
    [RequireComponent(typeof(TransportableObjectSO))]
    public abstract class Transportable : MonoBehaviour
    {
        protected const float PICKUP_SPEED = 0.5f;
        protected const float OFFSET = .85f;
        
        public abstract TransportableObjectSO TransportableObject { get; }
        
        public abstract void PickUp();

        public abstract void Drop();
    }
}
