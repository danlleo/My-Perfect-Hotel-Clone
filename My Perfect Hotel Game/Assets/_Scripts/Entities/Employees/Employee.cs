using UnityEngine;
using Utilities;

namespace Entities.Employees
{
    public abstract class Employee : Entity
    {
        [SerializeField] private Transform _idlePoint;

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsNullValue(this, nameof(_idlePoint), _idlePoint);
        }
#endif
    }
}
