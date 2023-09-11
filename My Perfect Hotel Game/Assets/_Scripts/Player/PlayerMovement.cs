using Misc;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const float PLAYER_RADIUS = .5f;
        private const float PLAYER_HEIGHT = 1f;
        
        [SerializeField] private float _moveSpeed = 4f;
        private Camera _camera;
        private Vector2 _startFingerTouchedPosition;
        private Vector2 _endFingerTouchedPosition;
        private bool _isWalking;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (PlayerInputHandler.IsMouseButtonDownThisFrame())
            {
                _startFingerTouchedPosition = Input.mousePosition;
            }

            if (PlayerInputHandler.IsMouseButtonHeldThisFrame())
            {
                _endFingerTouchedPosition = Input.mousePosition;

                if (Vector2.Distance(_startFingerTouchedPosition, _endFingerTouchedPosition) > 0f)
                {
                    HandleMovement();
                }
            }

            if (!PlayerInputHandler.IsMouseButtonUpThisFrame()) return;

            _startFingerTouchedPosition = Vector2.zero;
            _endFingerTouchedPosition = Vector2.zero;
        }

        // private void Move(Vector3 direction)
        // {
        //     transform.position += direction * (Time.deltaTime * _moveSpeed);
        // }

        private void HandleMovement() 
        {
            float moveDistance = _moveSpeed * Time.deltaTime;
            Vector3 playerBottomPoint = transform.position;
            Vector3 playerTopPoint = playerBottomPoint + Vector3.up * PLAYER_HEIGHT;
            var screenSpaceDirection = (_endFingerTouchedPosition - _startFingerTouchedPosition).normalized;
            var worldSpaceDirection = new Vector3(screenSpaceDirection.x, transform.position.y,
                screenSpaceDirection.y);

            bool canMove = !Physics.CapsuleCast(playerBottomPoint, playerTopPoint, PLAYER_RADIUS, worldSpaceDirection, moveDistance);

            if (!canMove) 
            {
                var moveDirX = new Vector3(worldSpaceDirection.x, 0f, 0f);
                canMove = worldSpaceDirection.x is < -0.5f or > +0.5f && !Physics.CapsuleCast(playerBottomPoint, playerTopPoint, PLAYER_RADIUS, moveDirX, moveDistance);

                if (canMove) 
                    worldSpaceDirection = moveDirX;

                var moveDirZ = new Vector3(0f, 0f, worldSpaceDirection.z);
                canMove = worldSpaceDirection.z is < -0.5f or > +0.5f && !Physics.CapsuleCast(playerBottomPoint, playerTopPoint, PLAYER_RADIUS, moveDirZ, moveDistance);

                if (canMove) 
                    worldSpaceDirection = moveDirZ;
            }

            if (canMove) transform.position += _moveSpeed * Time.deltaTime * worldSpaceDirection;

            _isWalking = worldSpaceDirection != Vector3.zero;
            transform.forward = Vector3.Slerp(transform.forward, worldSpaceDirection, Time.deltaTime * _moveSpeed * 1.5f);
        }
    }
}
