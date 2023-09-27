using System;

namespace StaticEvents
{
    public static class OnAnyOutdoorsRoomTookPlayer
    {
        public static event EventHandler Event;

        public static void Call(object sender) => 
            Event?.Invoke(sender, EventArgs.Empty);
    }
}
