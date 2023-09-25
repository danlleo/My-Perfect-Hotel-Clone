using System;

namespace Interfaces
{
    public interface IEvent<T> where T : EventArgs
    {
        public event EventHandler<T> Event;

        public void Call(object sender, T eventArgs);
    }

    public interface IEvent
    {
        public event EventHandler Event;

        public void Call(object sender);
    }
}
