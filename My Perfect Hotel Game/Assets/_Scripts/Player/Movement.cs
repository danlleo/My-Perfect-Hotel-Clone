using Events;
using Misc;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(AnimationsController), typeof(PlayerWalkingStateChangedEvent))]
    [DisallowMultipleComponent]
    public class Movement : MonoBehaviour
    {
        private const float PLAYER_RADIUS = .5f;
        private const float PLAYER_HEIGHT = 1f;

        [SerializeField] private float _moveSpeed = 4f;
        [FormerlySerializedAs("_playerCollisionLayerMask")] [SerializeField] private LayerMask _collisionLayerMask;
        private Vector2 _startFingerTouchedPosition;
        private Vector2 _endFingerTouchedPosition;
        private bool _isWalking;
        private PlayerWalkingStateChangedEvent _playerWalkingStateChangedEvent;

        private void Awake()
        {
            _playerWalkingStateChangedEvent = GetComponent<PlayerWalkingStateChangedEvent>();
        }

        private void Update()
        {
            if (InputHandler.IsMouseButtonDownThisFrame())
                _startFingerTouchedPosition = Input.mousePosition;
            
            if (InputHandler.IsMouseButtonHeldThisFrame())
            {
                _endFingerTouchedPosition = Input.mousePosition;

                if (Vector2.Distance(_startFingerTouchedPosition, _endFingerTouchedPosition) > 0f)
                {
                    var screenSpaceDirection = (_endFingerTouchedPosition - _startFingerTouchedPosition).normalized;
                    var worldSpaceDirection = new Vector3(screenSpaceDirection.x, transform.position.y,
                        screenSpaceDirection.y);
                    
                    HandleMovement(worldSpaceDirection);
                    HandleRotation(worldSpaceDirection);
                }
            }

            if (!InputHandler.IsMouseButtonUpThisFrame()) return;

            _startFingerTouchedPosition = Vector2.zero;
            _endFingerTouchedPosition = Vector2.zero;
            _isWalking = false;
            
            _playerWalkingStateChangedEvent.Call(this, false);
        }

        /// <summary>
        /// Move player in the direction in which finger is facing  
        /// </summary>
        private void HandleMovement(Vector3 directionToMove)
        {
            float moveDistance = _moveSpeed * Time.deltaTime;

            var playerBottomPoint = transform.position;
            var playerTopPoint = playerBottomPoint + Vector3.up * PLAYER_HEIGHT;
            
            bool canMove = !Physics.CapsuleCast(playerBottomPoint, playerTopPoint, PLAYER_RADIUS, directionToMove,
                moveDistance,_collisionLayerMask);
            
            if (canMove) transform.position += _moveSpeed * Time.deltaTime * directionToMove;

            _isWalking = true;
            _playerWalkingStateChangedEvent.Call(this, true);
        }

        /// <summary>
        ///  Rotate player towards his movement direction
        /// </summary>
        private void HandleRotation(Vector3 directionToRotate)
            => transform.forward = Vector3.Slerp(transform.forward, directionToRotate, Time.deltaTime * _moveSpeed * 1.5f);
    }
}