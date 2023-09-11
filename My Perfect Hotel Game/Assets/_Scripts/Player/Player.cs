using Events;
using UnityEngine;

namespace Player
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Interact))]
    [RequireComponent(typeof(AnimationsController))]
    [RequireComponent(typeof(PlayerWalkingStateChangedEvent))]
    public class Player : MonoBehaviour
    {
        [HideInInspector] public PlayerWalkingStateChangedEvent WalkingStateChangedEvent;

        private void Awake()
        {
            WalkingStateChangedEvent = GetComponent<PlayerWalkingStateChangedEvent>();
        }
    }
}
