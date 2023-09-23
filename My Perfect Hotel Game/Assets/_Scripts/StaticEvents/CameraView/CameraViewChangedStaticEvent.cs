using System;

namespace StaticEvents.CameraView
{
    public static class CameraViewChangedStaticEvent
    {
        public static event Action<CameraViewChangedStaticEventArgs> OnCameraViewChanged;

        public static void CallCameraViewChangedEvent(bool isInRoomArea, bool isLeftSided)
            => OnCameraViewChanged?.Invoke(new CameraViewChangedStaticEventArgs(isInRoomArea, isLeftSided));
    }

    public class CameraViewChangedStaticEventArgs : EventArgs
    {
        public readonly bool IsInRoomArea;
        public readonly bool IsLeftSided;
        
        public CameraViewChangedStaticEventArgs(bool isInRoomArea, bool isLeftSided)
        {
            IsInRoomArea = isInRoomArea;
            IsLeftSided = isLeftSided;
        }
    }
}
