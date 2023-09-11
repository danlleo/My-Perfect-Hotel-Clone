using System;
using UnityEngine;

namespace UI.Helpers
{
    public class WorldSpaceHoldUI : MonoBehaviour
    {
        private Vector3 _stayAtScreenPosition;
        private Vector3 _worldPosition;
        private Action _onComplete;
        private Camera _camera;
        
        private float _displayTime;
        private float _timer;

        private void Awake()
            => _camera = Camera.main;

        public void Initialize(Vector3 worldPosition, float displayTime, Action onComplete)
        {
            _worldPosition = worldPosition;
            _displayTime = displayTime;
            _onComplete = onComplete;
        }

        private void Update()
        {
            DisplayAtScreenPosition();
        }

        /// <summary>
        /// This method will keep UI object in the screen position converted from worldPosition.
        /// If camera is moving, UI object will still be displayed correctly.
        /// </summary>
        private void DisplayAtScreenPosition()
        {
            transform.position = _stayAtScreenPosition;
            _stayAtScreenPosition = _camera.WorldToScreenPoint(_worldPosition);
            _timer += Time.deltaTime;

            if (!(_timer > _displayTime)) return;
            
            _onComplete?.Invoke();
            ResetTimer();
        }

        private void ResetTimer()
            => _timer = 0f;
    }
}
