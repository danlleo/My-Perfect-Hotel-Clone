using StaticEvents;
using UnityEngine;

namespace Areas
{
    [DisallowMultipleComponent]
    public class OutdoorsTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
                OnAnyOutdoorsRoomTookPlayer.Call(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
                OnAnyOutdoorsRoomLostPlayer.Call(this);
        }
    }
}
