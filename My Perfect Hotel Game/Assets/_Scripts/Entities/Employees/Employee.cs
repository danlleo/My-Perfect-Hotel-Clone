using UnityEngine;

namespace Entities.Employees
{
    public abstract class Employee : Entity
    {
        [SerializeField] private Transform _idlePoint;
    }
}
