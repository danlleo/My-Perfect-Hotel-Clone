using UnityEngine;

namespace Room
{
    public class Room : MonoBehaviour
    {
        public bool IsAvailable { get; private set; }

        [SerializeField] private Transform _bedTransform;

        private void Awake()
        {
            // Hardcoded for now, change later
            IsAvailable = true;
        }

        public Vector3 GetBedPosition()
            => _bedTransform.position;

        public void SetIsNotAvailable()
            => IsAvailable = false;

        public void SetIsAvailable()
            => IsAvailable = true;
    }
}
