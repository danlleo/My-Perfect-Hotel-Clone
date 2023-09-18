using UnityEngine;
using Utilities;

namespace Entities.Employees
{
    public abstract class Employee : Entity
    {
        [SerializeField] protected Transform IdlePoint;

        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsNullValue(this, nameof(IdlePoint), IdlePoint);
        }
#endif

        #endregion
    }
}
