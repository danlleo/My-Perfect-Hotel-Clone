using System;

namespace StaticEvents.Room
{
    public static class RoomBecameAvailableToCleanStaticEvent
    {
        public static event Action<RoomBecameAvailableToCleanStaticEventArgs> OnRoomBecameAvailableToClean;

        public static void CallRoomBecameAvailableToCleanEvent(global::Room.Room room)
            => OnRoomBecameAvailableToClean?.Invoke(new RoomBecameAvailableToCleanStaticEventArgs(room));
    }
    
    public class RoomBecameAvailableToCleanStaticEventArgs : EventArgs
    {
        public readonly global::Room.Room Room;

        public RoomBecameAvailableToCleanStaticEventArgs(global::Room.Room room)
        {
            Room = room;
        }
    }
}