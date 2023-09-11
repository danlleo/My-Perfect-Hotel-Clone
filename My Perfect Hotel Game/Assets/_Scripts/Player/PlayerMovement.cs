using Misc;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
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
            const float playerRadius = .5f;
            const float playerHeight = 1f;
            Vector3 point1 = transform.position;
            Vector3 point2 = point1 + Vector3.up * playerHeight;
            var screenSpaceDirection = (_endFingerTouchedPosition - _startFingerTouchedPosition).normalized;
            var worldSpaceDirection = new Vector3(screenSpaceDirection.x, transform.position.y,
                screenSpaceDirection.y);

            bool canMove = !Physics.CapsuleCast(point1, point2, playerRadius, worldSpaceDirection, moveDistance);

            if (!canMove) 
            {
                var moveDirX = new Vector3(worldSpaceDirection.x, 0f, 0f);
                canMove = worldSpaceDirection.x is < -0.5f or > +0.5f && !Physics.CapsuleCast(point1, point2, playerRadius, moveDirX, moveDistance);

                if (canMove) worldSpaceDirection = moveDirX;

                var moveDirZ = new Vector3(0f, 0f, worldSpaceDirection.z);
                canMove = worldSpaceDirection.z is < -0.5f or > +0.5f && !Physics.CapsuleCast(point1, point2, playerRadius, moveDirZ, moveDistance);

                if (canMove) worldSpaceDirection = moveDirZ;
            }

            if (canMove) transform.position += _moveSpeed * Time.deltaTime * worldSpaceDirection;

            _isWalking = worldSpaceDirection != Vector3.zero;
            transform.forward = Vector3.Slerp(transform.forward, worldSpaceDirection, Time.deltaTime * _moveSpeed * 1.5f);
        }
    }
}
