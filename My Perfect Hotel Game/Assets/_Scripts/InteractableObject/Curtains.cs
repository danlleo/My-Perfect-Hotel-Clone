using System;
using UI.Helpers;
using UnityEngine;

namespace InteractableObject
{
    /// <summary>
    /// Hardcoded class for now
    /// </summary>
    public class Curtains : Interactable
    {
        [SerializeField] private Room.Room _room;
        [SerializeField] private ProgressBarUI _progressBarUI;

        private bool _isInteractable;
        
        private readonly float _interactTimeInSeconds = 1.2f;
        private float _timer;

        private void Awake()
        {
            _progressBarUI.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _room.LeftRoomEvent.Event += Room_LeftRoomEvent;
        }

        private void OnDisable()
        {
            _room.LeftRoomEvent.Event -= Room_LeftRoomEvent;
        }
        
        public override void Interact()
        {
            if (!_isInteractable)
                return;
            
            _progressBarUI.UpdateProgressBar(_timer, _interactTimeInSeconds);
            IncreaseTimer();
            
            if (!(_timer >= _interactTimeInSeconds)) return;
            
            // Perform action when timer is over
            Clean();
            ResetTimer();
        }
        
        public override bool TryInteractWithCallback(out Action onComplete)
        {
            onComplete = null;
            
            if (!_isInteractable)
                return false;
            
            _progressBarUI.UpdateProgressBar(_timer, _interactTimeInSeconds);
            IncreaseTimer();
            
            if (!(_timer >= _interactTimeInSeconds)) return false;
            
            // Perform action when timer is over
            onComplete = () =>
            {
                // TODO: Add needed action
                Clean();
                ResetTimer();
            };
            
            return true;
        }

        private void Room_LeftRoomEvent(object sender, EventArgs e)
        {
            _isInteractable = true;
            _progressBarUI.gameObject.SetActive(true);
        }

        private void Clean()
        {
            _isInteractable = false;
            
            _room.TryFinishRoomCleaning(this);
            _progressBarUI.ClearProgressBar();
            _progressBarUI.gameObject.SetActive(false);
        }
        
        private void IncreaseTimer()
            => _timer += Time.deltaTime;

        private void ResetTimer()
            => _timer = 0f;
    }
}
