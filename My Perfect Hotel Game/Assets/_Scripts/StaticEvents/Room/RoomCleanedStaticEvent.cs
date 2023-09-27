using System;

namespace StaticEvents.Room
{
    public static class RoomCleanedStaticEvent
    {
        public static event Action<RoomCleanedStaticEventArgs> OnRoomCleaned;

        public static void CallRoomCleanedEvent(Areas.Room room)
            => OnRoomCleaned?.Invoke(new RoomCleanedStaticEventArgs(room));
    }

    public class RoomCleanedStaticEventArgs : EventArgs
    {
        public readonly Areas.Room Room;

        public RoomCleanedStaticEventArgs(Areas.Room room)
        {
            Room = room;
        }
    }
}