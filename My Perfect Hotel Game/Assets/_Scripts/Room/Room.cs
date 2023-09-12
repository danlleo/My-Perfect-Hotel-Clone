using UnityEngine;

namespace Room
{
    public class Room : MonoBehaviour
    {
        [HideInInspector] public bool IsAvailable { get; private set; }

        [SerializeField] private Transform _bedTransform;

        public Vector3 GetBedPosition()
            => _bedTransform.position;
    }
}
