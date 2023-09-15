using System;
using UnityEngine;

namespace InteractableObject
{
    /// <summary>
    /// Hardcoded class for now
    /// </summary>
    public class Curtains : Interactable
    {
        [SerializeField] private Room.Room _room;

        private bool _isInteractable;
        
        private readonly float _interactTimeInSeconds = 1.2f;
        private float _timer;
        
        private void OnEnable()
        {
            _room.LeftRoomEvent.Event += Room_LeftRoomEvent;
        }

        private void OnDisable()
        {
            _room.LeftRoomEvent.Event -= Room_LeftRoomEvent;
        }

        private void Update()
        {
            if (!(_timer >= _interactTimeInSeconds)) return;

            Clean();
            ResetTimer();
        }

        public override void Interact()
        {
            if (!_isInteractable)
                return;
            
            IncreaseTimer();
        }

        private void Room_LeftRoomEvent(object sender, EventArgs e)
        {
            _isInteractable = true;
        }

        private void Clean()
        {
            _isInteractable = false;
            
            _room.TryFinishRoomCleaning(this);
        }
        
        private void IncreaseTimer()
            => _timer += Time.deltaTime;

        private void ResetTimer()
            => _timer = 0f;
    }
}
