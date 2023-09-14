using Events;
using UnityEngine;

namespace Player
{
    [SelectionBase]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Interact))]
    [RequireComponent(typeof(AnimationsController))]
    [RequireComponent(typeof(PlayerWalkingStateChangedEvent))]
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        public const float RADIUS = .5f;
        public const float HEIGHT = 1f;
        
        [HideInInspector] public PlayerWalkingStateChangedEvent WalkingStateChangedEvent;

        private void Awake()
        {
            WalkingStateChangedEvent = GetComponent<PlayerWalkingStateChangedEvent>();
        }
    }
}
