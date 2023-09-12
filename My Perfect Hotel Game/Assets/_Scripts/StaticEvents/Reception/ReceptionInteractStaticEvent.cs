using System;

namespace StaticEvents.Reception
{
    public static class ReceptionInteractStaticEvent
    {
        public static event Action<ReceptionInteractStaticEventArgs> OnReceptionInteracted;

        public static void CallReceptionInteractedEvent(global::Reception.Reception interactedReception)
            => OnReceptionInteracted?.Invoke(new ReceptionInteractStaticEventArgs(interactedReception));
    }

    public class ReceptionInteractStaticEventArgs : EventArgs
    {
        public global::Reception.Reception InteractedReception;

        public ReceptionInteractStaticEventArgs(global::Reception.Reception reception)
            => InteractedReception = reception;
    }
}