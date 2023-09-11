using System;

namespace Interfaces
{
    public interface IEvent
    {
        public event EventHandler<bool> Event;

        public void Call(object sender, bool isWalking);
    }
}
