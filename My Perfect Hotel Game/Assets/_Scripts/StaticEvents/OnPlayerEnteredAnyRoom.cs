using System;

namespace StaticEvents
{
    public static class OnPlayerEnteredAnyRoom
    {
        public static event EventHandler<OnPlayerEnteredAnyRoomEventArgs> Event;

        public static void Call(object sender, OnPlayerEnteredAnyRoomEventArgs e)
            => Event?.Invoke(sender, e);
    }

    public class OnPlayerEnteredAnyRoomEventArgs : EventArgs
    {
        public readonly bool IsOutside;
        
        public OnPlayerEnteredAnyRoomEventArgs(bool isOutside)
        {
            IsOutside = isOutside;
        }
    }
}
