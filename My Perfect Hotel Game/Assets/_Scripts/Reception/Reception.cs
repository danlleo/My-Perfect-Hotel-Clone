using System;
using InteractableObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace Reception
{
    [DisallowMultipleComponent]
    public class Reception : Interactable
    {
        [FormerlySerializedAs("_queueLimit")] [SerializeField] private int _guestInHotelLimit = 4;

        private int _guestInHotelCount;

        private readonly float _interactTimeInSeconds = 1.2f;
        private float _timer;
        
        public override void Interact()
        {
            if (_guestInHotelCount >= _guestInHotelLimit)
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
