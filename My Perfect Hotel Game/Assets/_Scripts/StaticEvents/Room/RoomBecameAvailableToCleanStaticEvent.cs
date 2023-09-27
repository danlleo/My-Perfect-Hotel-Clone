using System;

namespace StaticEvents.Room
{
    public static class RoomBecameAvailableToCleanStaticEvent
    {
        public static event Action<RoomBecameAvailableToCleanStaticEventArgs> OnRoomBecameAvailableToClean;

        public static void CallRoomBecameAvailableToCleanEvent(Areas.Room room)
            => OnRoomBecameAvailableToClean?.Invoke(new RoomBecameAvailableToCleanStaticEventArgs(room));
    }
    
    public class RoomBecameAvailableToCleanStaticEventArgs : EventArgs
    {
        public readonly Areas.Room Room;

        public RoomBecameAvailableToCleanStaticEventArgs(Areas.Room room)
        {
            Room = room;
        }
    }
}