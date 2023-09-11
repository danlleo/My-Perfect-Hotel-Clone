using System;
using InteractableObject;
using UnityEngine;

namespace Reception
{
    [DisallowMultipleComponent]
    public class Reception : Interactable
    {
        [SerializeField] private int _queueLimit = 4;

        private int _currentQueueCount;

        private readonly float _interactTimeInSeconds = 1.2f;
        private float _timer;
        
        public override void Interact()
        {
            if (_currentQueueCount >= _queueLimit)
                return;

            IncreaseTimer();
        }

        private void Update()
        {
            if (!(_timer >= _interactTimeInSeconds)) return;
            
            PerformAction(() => print("Action performed"));
            ResetTimer();
        }

        private void PerformAction(Action action)
            => action?.Invoke();
        
        private void IncreaseTimer()
            => _timer += Time.deltaTime;

        private void ResetTimer()
            => _timer = 0f;
    }
}
