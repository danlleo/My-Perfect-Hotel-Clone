using System;
using System.Collections;
using UnityEngine;

namespace UI.Helpers
{
    [DisallowMultipleComponent]
    public class WorldSpaceHoldUI : MonoBehaviour
    {
        private Vector3 _stayAtScreenPosition;
        private Vector3 _worldPosition;
        private Action _onComplete;
        private Camera _camera;
        
        private float _displayTimeInSeconds;

        private void Awake()
            => _camera = Camera.main;
        
        public void Initialize(Vector3 worldPosition, float displayTimeInSeconds, Action onComplete)
        {
            _worldPosition = worldPosition;
            _displayTimeInSeconds = displayTimeInSeconds;
            _onComplete = onComplete;

            StartCoroutine(PerformActionAfterTimeRoutine(_onComplete));
        }

        public void Initialize(Vector3 worldPosition)
            => _worldPosition = worldPosition;

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
            _stayAtScreenPosition = _camera.WorldToScreenPoint(_worldPosition);
            transform.position = _stayAtScreenPosition;
        }

        /// <summary>
        /// If user specified Initialize with onComplete delegate, it will run once after specific period of time
        /// </summary>
        private IEnumerator PerformActionAfterTimeRoutine(Action onComplete)
        {
            yield return new WaitForSeconds(_displayTimeInSeconds);
            onComplete?.Invoke();
        }
    }
}
