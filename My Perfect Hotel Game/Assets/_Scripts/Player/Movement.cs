using Events;
using Misc;
using UnityEngine;
using Utilities;

namespace Player
{
    [DisallowMultipleComponent]
    public class Movement : MonoBehaviour
    {
        [Tooltip("Populate with the speed of how fast player will move")]
        [SerializeField] private float _moveSpeed = 4f;

        [Tooltip("Populate with the speed that of how fast player will rotate")] 
        [SerializeField] private float _rotateSpeed = 6f;
        
        [Tooltip("Populate with layers that will be detected as collision, to prevent player through moving them")]
        [SerializeField] private LayerMask _collisionLayerMask;
        
        private Player _player;
        
        private Vector3 _moveDirection;
        private Vector2 _startFingerTouchPosition;
        private Vector2 _heldFingerTouchedPosition;
        
        private bool _isWalking;

        private void Awake()
            => _player = GetComponent<Player>();

        private void Update()
        {
            if (InputHandler.IsMouseButtonDownThisFrame())
                _startFingerTouchPosition = Input.mousePosition;
            
            if (InputHandler.IsMouseButtonHeldThisFrame())
            {
                _heldFingerTouchedPosition = Input.mousePosition;

                if (Vector2.Distance(_startFingerTouchPosition, _heldFingerTouchedPosition) > 0f)
                {
                    Vector2 screenSpaceDirection = (_heldFingerTouchedPosition - _startFingerTouchPosition).normalized;
                    _moveDirection = new Vector3(screenSpaceDirection.x, 0f, screenSpaceDirection.y);

                    HandleMovement(_moveDirection);
                    HandleRotation(_moveDirection);
                }
            }

            if (!InputHandler.IsMouseButtonUpThisFrame()) 
                return;

            _startFingerTouchPosition = Vector2.zero;
            _heldFingerTouchedPosition = Vector2.zero;
            _isWalking = false;
            
            _player.WalkingStateChangedEvent.Call(this, new PlayerWalkingStateChangedEventArgs(isWalking: _isWalking));
        }

        public bool IsWalking() => _isWalking;
        
        /// <summary>
        /// Move player in the direction in which finger is facing  
        /// </summary>
        private void HandleMovement(Vector3 directionToMove)
        {
            float moveDistance = _moveSpeed * Time.deltaTime;

            Vector3 playerBottomPoint = transform.position;
            Vector3 playerTopPoint = playerBottomPoint + Vector3.up * Player.HEIGHT;
            
            // First time trying to detect the wall with the capsule cast
            bool canMove = !Physics.CapsuleCast(playerBottomPoint, playerTopPoint, Player.RADIUS, directionToMove,
                moveDistance,_collisionLayerMask);

            // If we detected wall, then we can't move. Let's check if we can't move horizontally
            if (!canMove)
            {
                // We create a horizontal direction vector
                Vector3 moveDirectionX = new Vector3(_moveDirection.x, 0f, 0f).normalized;

                // We're setting up 'canMove' boolean value again.
                // If direction vector is present, and we didn't detect the obstacle on the sides, then we can move.
                // Otherwise we can't
                canMove = _moveDirection.x != 0 && !Physics.CapsuleCast(playerBottomPoint, playerTopPoint,
                    Player.RADIUS, moveDirectionX, moveDistance);

                // If we can move, we just set _moveDirection as horizontal direction
                if (canMove)
                    _moveDirection = moveDirectionX;
                else
                {
                    // Otherwise we try to create a forward direction vector
                    Vector3 moveDirectionZ = new Vector3(0f, 0f, _moveDirection.z);
                    
                    // We again check if direction vector is present, and we didn't detect the obstacle on the sides, then we can move.
                    canMove = _moveDirection.z != 0 && !Physics.CapsuleCast(playerBottomPoint, playerTopPoint,
                        Player.RADIUS, moveDirectionZ, moveDistance);

                    // If we can move, then set _moveDirection as forward direction
                    if (canMove)
                        _moveDirection = moveDirectionZ;
                }
            }

            if (canMove)
                transform.position += moveDistance * _moveDirection;

            _isWalking = true;
            
            _player.WalkingStateChangedEvent.Call(this, new PlayerWalkingStateChangedEventArgs(isWalking: _isWalking));
        }

        /// <summary>
        ///  Rotate player towards his movement direction
        /// </summary>
        private void HandleRotation(Vector3 directionToRotate)
            => transform.forward = Vector3.Slerp(transform.forward, directionToRotate, Time.deltaTime * _rotateSpeed);

        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsPositiveValue(this, nameof(_rotateSpeed), _rotateSpeed);
            EditorValidation.IsPositiveValue(this, nameof(_moveSpeed), _moveSpeed);
            EditorValidation.IsPositiveValue(this, nameof(_collisionLayerMask), _collisionLayerMask);
        }
#endif

        #endregion
    }
}
