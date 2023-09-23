using System;
using Entities.Customer;
using InteractableObject;
using Room;
using StaticEvents.Reception;
using UI.Helpers;
using UnityEngine;
using Utilities;

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
            _progressBarUI.UpdateProgressBar(_timer, _interactTimeInSeconds);
            IncreaseTimer();
            
            if (!(_timer >= _interactTimeInSeconds)) 
                return;
            
            // Perform action when timer is over
            ReceptionInteractStaticEvent.CallReceptionInteractedEvent(this, ResetTimer);
        }

        public override bool TryInteractWithCallback(out Action onComplete)
        {
            onComplete = null;
            
            _progressBarUI.UpdateProgressBar(_timer, _interactTimeInSeconds);
            IncreaseTimer();
            
            if (!(_timer >= _interactTimeInSeconds)) 
                return false;
            
            // Perform action when timer is over
            ReceptionInteractStaticEvent.CallReceptionInteractedEvent(this, ResetTimer);
            
            return true;
        }

        /// <summary>
        /// This method is called when guest is standing first in the line, and there's available room for him to appoint
        /// </summary>
        public void AppointGuestToRoom(Customer customer)
        {
            Room.Room room = RoomManager.Instance.GetAvailableRoom();
            room.OccupyRoomWithGuest(customer);
            
            customer.SetRoom(room);
            customer.CustomerAppointedEvent.Call(this);
        }
        
        private void IncreaseTimer()
            => _timer += Time.deltaTime;

        private void ResetTimer()
            => _timer = 0f;

        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsPositiveValue(this, nameof(_guestInHotelLimit), _guestInHotelLimit, false);
            EditorValidation.IsNullValue(this, nameof(_progressBarUI), _progressBarUI);
        }
#endif

        #endregion
    }
}
