using Misc;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const float PLAYER_RADIUS = .5f;
        private const float PLAYER_HEIGHT = 1f;
        
        [SerializeField] private float _moveSpeed = 4f;
                
        private Vector2 _startFingerTouchedPosition;
        private Vector2 _endFingerTouchedPosition;
        
        private bool _isWalking;
        
        private void Update()
        {
            if (PlayerInputHandler.IsMouseButtonDownThisFrame())
                _startFingerTouchedPosition = Input.mousePosition;
            
            if (PlayerInputHandler.IsMouseButtonHeldThisFrame())
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

            if (!PlayerInputHandler.IsMouseButtonUpThisFrame()) return;

            _startFingerTouchedPosition = Vector2.zero;
            _endFingerTouchedPosition = Vector2.zero;
        }

        /// <summary>
        /// Move player in the direction in which finger is facing  
        /// </summary>
        private void HandleMovement(Vector3 directionToMove)
        {
            var moveDistance = _moveSpeed * Time.deltaTime;

            var playerBottomPoint = transform.position;
            var playerTopPoint = playerBottomPoint + Vector3.up * PLAYER_HEIGHT;
            
            var canMove = !Physics.CapsuleCast(playerBottomPoint, playerTopPoint, PLAYER_RADIUS, directionToMove,
                moveDistance);
            
            if (canMove) transform.position += _moveSpeed * Time.deltaTime * directionToMove;

            _isWalking = directionToMove != Vector3.zero;
        }

        /// <summary>
        ///  Rotate player towards his movement direction
        /// </summary>
        private void HandleRotation(Vector3 directionToRotate)
            => transform.forward = Vector3.Slerp(transform.forward, directionToRotate, Time.deltaTime * _moveSpeed * 1.5f);
    }
}