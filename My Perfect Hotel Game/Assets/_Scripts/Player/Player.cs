using Events;
using Surface.Material;
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
    [RequireComponent(typeof(PlayerMadeAStep))]
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        public const float RADIUS = .5f;
        public const float HEIGHT = 1f;
        
        [HideInInspector] public PlayerWalkingStateChangedEvent WalkingStateChangedEvent;
        [HideInInspector] public PlayerPickedAnObjectEvent PickedAnObjectEvent;
        [HideInInspector] public PlayerDroppedAnObjectEvent DroppedAnObjectEvent;
        [HideInInspector] public PlayerMadeAStep StepEvent;

        [SerializeField] private LayerMask _floorLayerMask;
        private Inventory _inventory;
        private Movement _movement;
        private Surface.Surface _surfaceMaterial;

        private void Awake()
        {
            WalkingStateChangedEvent = GetComponent<PlayerWalkingStateChangedEvent>();
            PickedAnObjectEvent = GetComponent<PlayerPickedAnObjectEvent>();
            DroppedAnObjectEvent = GetComponent<PlayerDroppedAnObjectEvent>();
            StepEvent = GetComponent<PlayerMadeAStep>();
            _inventory = GetComponent<Inventory>();
            _movement = GetComponent<Movement>();
        }

        private void Update() 
            => UpdateSurface();
        
        private void UpdateSurface()
        {
            if (!_movement.IsWalking()) return;
            
            if (!Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, HEIGHT, _floorLayerMask))
                return;

            if (!hitInfo.transform.TryGetComponent(out Surface.Surface surface))
            {
                Debug.LogWarning($"{hitInfo.transform.gameObject} doesn't have Surface material assigned");
                hitInfo.transform.gameObject.AddComponent<Wood>();
            }
            
            _surfaceMaterial = surface;
        }

        public Inventory GetInventory() => _inventory;

        public Movement GetMovement() => _movement;

        public Surface.Surface GetSurface() => _surfaceMaterial;
    }
}
