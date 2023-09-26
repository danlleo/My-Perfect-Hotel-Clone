using System;
using Enums;
using Interfaces;

namespace StaticEvents.CameraView
{
    public static class CameraViewChangedStaticEvent
    {
        public static event Action<CameraViewChangedStaticEventArgs> OnCameraViewChanged;

        public static void CallCameraViewChangedEvent(bool isInRoomArea, RoomDirection roomDirection)
            => OnCameraViewChanged?.Invoke(new CameraViewChangedStaticEventArgs(isInRoomArea, roomDirection));
    }

    public class CameraViewChangedStaticEventArgs : EventArgs
    {
        public readonly bool IsInRoomArea;
        public readonly RoomDirection RoomDirection;
        
        public CameraViewChangedStaticEventArgs(bool isInRoomArea, RoomDirection roomDirection)
        {
            IsInRoomArea = isInRoomArea;
            RoomDirection = roomDirection;
        }
    }
}
