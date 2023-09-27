using System;

namespace StaticEvents
{
    public class OnAnyOutdoorsRoomLostPlayer
    {
        public static event EventHandler Event;

        public static void Call(object sender) =>
            Event?.Invoke(sender, EventArgs.Empty);
    }
}
