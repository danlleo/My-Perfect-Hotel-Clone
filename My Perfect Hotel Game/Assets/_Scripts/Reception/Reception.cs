using InteractableObject;
using Room;
using StaticEvents.Reception;
using UI.Helpers;
using UnityEngine;

namespace Reception
{
    [DisallowMultipleComponent]
    public class Reception : Interactable
    {
        [SerializeField] private int _guestInHotelLimit = 4;
        [SerializeField] private ProgressBarUI _progressBarUI;
        
        private int _guestInHotelCount;

        private readonly float _interactTimeInSeconds = 1.2f;
        private float _timer;
        
        /// <summary>
        /// This method will be called when player/servant is close to the counter and interacting with it.
        /// </summary>
        public override void Interact()
        {
            if (_guestInHotelCount >= _guestInHotelLimit)
                return;
            
            _progressBarUI.UpdateProgressBar(_timer, _interactTimeInSeconds);
            IncreaseTimer();
            
            if (!(_timer >= _interactTimeInSeconds)) return;
            
            // Perform action when timer is over
            ReceptionInteractStaticEvent.CallReceptionInteractedEvent(this, ResetTimer);
        }

        public void AppointGuestToRoom(Guest.Guest guest)
        {
            var room = RoomManager.Instance.GetAvailableRoom();
            room.SetIsNotAvailable();
            
            guest.SetRoom(room);
            guest.GuestAppointedEvent.Call(this);
        }
        
        private void IncreaseTimer()
            => _timer += Time.deltaTime;

        private void ResetTimer()
            => _timer = 0f;
    }
}
