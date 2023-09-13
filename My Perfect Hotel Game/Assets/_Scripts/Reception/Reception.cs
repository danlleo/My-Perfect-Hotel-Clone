using System;
using InteractableObject;
using Room;
using StaticEvents.Reception;
using UnityEngine;

namespace Reception
{
    [DisallowMultipleComponent]
    public class Reception : Interactable
    {
        [SerializeField] private int _guestInHotelLimit = 4;
        
        private int _guestInHotelCount;

        private readonly float _interactTimeInSeconds = 1.2f;
        private float _timer;
        
        private void Update()
        {
            if (!(_timer >= _interactTimeInSeconds)) return;
            
            PerformAction(() => ReceptionInteractStaticEvent.CallReceptionInteractedEvent(this));
            ResetTimer();
        }
        
        /// <summary>
        /// This method will be called when player/servant is close to the counter and interacting with it.
        /// </summary>
        public override void Interact()
        {
            if (_guestInHotelCount >= _guestInHotelLimit)
                return;

            IncreaseTimer();
        }

        public void AppointGuestToRoom(Guest.Guest guest)
        {
            var room = RoomManager.Instance.GetAvailableRoom();
            room.SetIsNotAvailable();
            
            guest.SetRoom(room);
            guest.GuestAppointedEvent.CallGuestAppointedEvent();
        }
        
        /// <summary>
        ///  Perform action after interact time is met
        /// </summary>
        private void PerformAction(Action action)
            => action?.Invoke();
        
        private void IncreaseTimer()
            => _timer += Time.deltaTime;

        private void ResetTimer()
            => _timer = 0f;
    }
}
