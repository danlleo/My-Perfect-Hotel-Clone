using Enums;
using StaticEvents.CameraView;
using UnityEngine;

namespace CameraView
{
    [DisallowMultipleComponent]
    public class AdjusterTrigger : MonoBehaviour
    {
        [SerializeField] private RoomDirection _roomDirection;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
                CameraViewChangedStaticEvent.CallCameraViewChangedEvent(true, _roomDirection);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
                CameraViewChangedStaticEvent.CallCameraViewChangedEvent(false, _roomDirection);
        }
    }
}
