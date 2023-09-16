using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        protected abstract Vector3 GetNextDestination();
    }
}
