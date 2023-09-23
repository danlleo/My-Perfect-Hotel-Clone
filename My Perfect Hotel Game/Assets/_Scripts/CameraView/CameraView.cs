using DG.Tweening;
using StaticEvents.CameraView;
using UnityEngine;

namespace CameraView
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private float _angle = 35f;
        [SerializeField] private float _rotateDuration = .20f;

        private Vector3 _initialRotation;

        private void Awake()
            => _initialRotation = transform.eulerAngles;

        private void OnEnable()
        {
            CameraViewChangedStaticEvent.OnCameraViewChanged += CameraViewChangedStatic_Event;
        }

        private void OnDisable()
        {
            CameraViewChangedStaticEvent.OnCameraViewChanged -= CameraViewChangedStatic_Event;
        }

        #region Events

        private void CameraViewChangedStatic_Event(CameraViewChangedStaticEventArgs cameraViewChangedStaticEventArgs)
        {
            if (cameraViewChangedStaticEventArgs.IsInRoomArea)
            {
                transform.DORotate(
                    cameraViewChangedStaticEventArgs.IsLeftSided
                        ? new Vector3(_initialRotation.x, -_angle, _initialRotation.z)
                        : new Vector3(_initialRotation.x, _angle, _initialRotation.z), _rotateDuration
                );

                return;
            }

            transform.DORotate(_initialRotation, _rotateDuration);
        }

        #endregion
    }
}
