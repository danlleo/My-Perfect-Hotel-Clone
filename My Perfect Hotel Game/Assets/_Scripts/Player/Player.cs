using Events;
using UnityEngine;

namespace Player
{
    [SelectionBase]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Interact))]
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(AnimationsController))]
    [RequireComponent(typeof(PlayerWalkingStateChangedEvent))]
    [RequireComponent(typeof(PlayerPickedAnObjectEvent))]
    [RequireComponent(typeof(PlayerDroppedAnObjectEvent))]
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        public const float RADIUS = .5f;
        public const float HEIGHT = 1f;
        
        [HideInInspector] public PlayerWalkingStateChangedEvent WalkingStateChangedEvent;
        [HideInInspector] public PlayerPickedAnObjectEvent PickedAnObjectEvent;
        [HideInInspector] public PlayerDroppedAnObjectEvent DroppedAnObjectEvent;
        private Inventory _inventory;

        private void Awake()
        {
            WalkingStateChangedEvent = GetComponent<PlayerWalkingStateChangedEvent>();
            PickedAnObjectEvent = GetComponent<PlayerPickedAnObjectEvent>();
            DroppedAnObjectEvent = GetComponent<PlayerDroppedAnObjectEvent>();
            _inventory = GetComponent<Inventory>();
        }

        public Inventory GetInventory() => _inventory;
    }
}
