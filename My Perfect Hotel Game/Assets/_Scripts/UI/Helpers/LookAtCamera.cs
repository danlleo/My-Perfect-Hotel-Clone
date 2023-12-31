using UnityEngine;

namespace UI.Helpers
{
    [DisallowMultipleComponent]
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private bool _isInvert;

        private Transform _cameraTransform;
        private void Awake() 
            => _cameraTransform = Camera.main.transform;

        private void LateUpdate()
        {
            if (_isInvert)
            {
                var directionToCamera = (_cameraTransform.position - transform.position).normalized;
                transform.LookAt(transform.position + directionToCamera * -1);
            }
            else
            {
                transform.LookAt(_cameraTransform);
            }
        }
    }
}
