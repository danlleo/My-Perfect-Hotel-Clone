using System;

namespace StaticEvents.Reception
{
    public static class ReceptionInteractStaticEvent
    {
        public static event Action<ReceptionInteractStaticEventArgs> OnReceptionInteracted;

        public static void CallReceptionInteractedEvent(global::Reception.Reception interactedReception, Action onSuccess = null)
            => OnReceptionInteracted?.Invoke(new ReceptionInteractStaticEventArgs(interactedReception, onSuccess));
    }

    public class ReceptionInteractStaticEventArgs : EventArgs
    {
        public global::Reception.Reception InteractedReception;
        public Action OnSuccess;

        public ReceptionInteractStaticEventArgs(global::Reception.Reception reception, Action onSuccess)
        {
            InteractedReception = reception;
            OnSuccess = onSuccess;
        }
    }
}