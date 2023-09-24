using System;

namespace StaticEvents.Room
{
    public static class RoomCleanedStaticEvent
    {
        public static event Action<RoomCleanedStaticEventArgs> OnRoomCleaned;

        public static void CallRoomCleanedEvent(global::Room.Room room)
            => OnRoomCleaned?.Invoke(new RoomCleanedStaticEventArgs(room));
    }

    public class RoomCleanedStaticEventArgs : EventArgs
    {
        public readonly global::Room.Room Room;

        public RoomCleanedStaticEventArgs(global::Room.Room room)
        {
            Room = room;
        }
    }
}