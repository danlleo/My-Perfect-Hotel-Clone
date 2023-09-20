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
                    _moveDirection = new Vector3(screenSpaceDirection.x, transform.position.y,
                        screenSpaceDirection.y);
                    
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
        
        /// <summary>
        /// Move player in the direction in which finger is facing  
        /// </summary>
        private void HandleMovement(Vector3 directionToMove)
        {
            float moveDistance = _moveSpeed * Time.deltaTime;

            Vector3 playerBottomPoint = transform.position;
            Vector3 playerTopPoint = playerBottomPoint + Vector3.up * Player.HEIGHT;
            
            bool canMove = !Physics.CapsuleCast(playerBottomPoint, playerTopPoint, Player.RADIUS, directionToMove,
                moveDistance,_collisionLayerMask);

            if (!canMove)
            {
                var moveDirectionX = new Vector3(_moveDirection.x, 0f, 0f).normalized;

                canMove = _moveDirection.x != 0 && !Physics.CapsuleCast(playerBottomPoint, playerTopPoint,
                    Player.RADIUS, moveDirectionX, moveDistance);

                if (canMove)
                    _moveDirection = moveDirectionX;
                else
                {
                    var moveDirectionZ = new Vector3(0f, 0f, _moveDirection.z);

                    canMove = _moveDirection.z != 0 && !Physics.CapsuleCast(playerBottomPoint, playerTopPoint,
                        Player.RADIUS, moveDirectionZ, moveDistance);

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
            => transform.forward = Vector3.Slerp(transform.forward, directionToRotate, Time.deltaTime * _moveSpeed * 1.5f);

        #region Validation

#if UNITY_EDITOR
        private void OnValidate()
        {
            EditorValidation.IsPositiveValue(this, nameof(_moveSpeed), _moveSpeed);
            EditorValidation.IsPositiveValue(this, nameof(_collisionLayerMask), _collisionLayerMask);
        }
#endif

        #endregion
    }
}
