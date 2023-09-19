using UnityEngine;

namespace TransportableObjects
{
    public abstract class Transportable : MonoBehaviour
    {
        public const float PICKUP_SPEED = 0.5f;
        
        public abstract void PickUp();

        public abstract void Drop();
    }
}
