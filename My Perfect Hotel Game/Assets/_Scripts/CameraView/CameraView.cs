using System;
using Cinemachine;
using DG.Tweening;
using Enums;
using StaticEvents.CameraView;
using UnityEngine;

namespace CameraView
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private float _targetXRotationAngle = 55f;
        [SerializeField] private float _targetYRotationAngle = 35f;
        [SerializeField] private float _rotateDuration = .20f;
        [SerializeField] private float _zoomDuration = .30f;
        [SerializeField] private float _targetZoomInValue = 50f;
        
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private Vector3 _initialRotation;

        private float _initialZoomValue;
        
        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _initialZoomValue = _cinemachineVirtualCamera.m_Lens.FieldOfView;
            _initialRotation = transform.eulerAngles;
        }

        private void OnEnable()
        {
            CameraViewChangedStaticEvent.OnCameraViewChanged += CameraViewChangedStatic_Event;
        }

        private void OnDisable()
        {
            CameraViewChangedStaticEvent.OnCameraViewChanged -= CameraViewChangedStatic_Event;
        }

        private void RotateCameraTowardsAngles(Vector2 targetRotation)
        {
            transform.DORotate(new Vector3(targetRotation.x, targetRotation.y, _initialRotation.z),
                _rotateDuration);
        }
        
        private void ResetCameraRotation()
            => transform.DORotate(_initialRotation, _rotateDuration);
        
        private void ZoomIn()
        {
            DOVirtual.Float(_initialZoomValue, _targetZoomInValue, _zoomDuration, UpdateFOVValue)
                .SetEase(Ease.Linear);
        }

        private void ZoomOut()
        {
            float currentZoomValue = _cinemachineVirtualCamera.m_Lens.FieldOfView;
            
            DOVirtual.Float(currentZoomValue, _initialZoomValue, _zoomDuration, UpdateFOVValue)
                .SetEase(Ease.Linear);
        }
        
        private void UpdateFOVValue(float value)
            => _cinemachineVirtualCamera.m_Lens.FieldOfView = value;

        #region Events

        private void CameraViewChangedStatic_Event(CameraViewChangedStaticEventArgs cameraViewChangedStaticEventArgs)
        {
            if (cameraViewChangedStaticEventArgs.IsInRoomArea)
            {
                switch (cameraViewChangedStaticEventArgs.RoomDirection)
                {
                    case RoomDirection.North:
                        ZoomIn();
                        break;
                    case RoomDirection.East:
                        RotateCameraTowardsAngles(new Vector2(_targetXRotationAngle, _targetYRotationAngle));
                        break;
                    case RoomDirection.South:
                        throw new ArgumentOutOfRangeException();
                    case RoomDirection.West:
                        RotateCameraTowardsAngles(new Vector2(_targetXRotationAngle, -_targetYRotationAngle));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return;
            }

            ResetCameraRotation();
            ZoomOut();
        }

        #endregion
    }
}
