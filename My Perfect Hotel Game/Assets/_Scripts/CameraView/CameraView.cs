using StaticEvents.CameraView;
using UnityEngine;

namespace CameraView
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private float _angle = 35f;
        
        private void OnEnable()
        {
            CameraViewChangedStaticEvent.OnCameraViewChanged += CameraViewChangedStatic_Event;
        }

        private void OnDisable()
        {
            CameraViewChangedStaticEvent.OnCameraViewChanged -= CameraViewChangedStatic_Event;
        }

        private void CameraViewChangedStatic_Event(CameraViewChangedStaticEventArgs cameraViewChangedStaticEventArgs)
        {
            if (cameraViewChangedStaticEventArgs.IsInRoomArea)
            {
                
            }
        }
    }
}
