using _Scripts.Misc;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 4f;

        private Camera _camera;
        
        private Vector2 _startFingerTouchedPosition;
        private Vector2 _endFingerTouchedPosition;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (PlayerInputHandler.IsMouseButtonDownThisFrame())
            {
                _startFingerTouchedPosition = (Input.mousePosition);
            }

            if (PlayerInputHandler.IsMouseButtonHeldThisFrame())
            {
                _endFingerTouchedPosition = Input.mousePosition;

                if (Vector2.Distance(_startFingerTouchedPosition, _endFingerTouchedPosition) > 0f)
                {
                    var screenSpaceDirection = (_endFingerTouchedPosition - _startFingerTouchedPosition).normalized;
                    var worldSpaceDirection = new Vector3(screenSpaceDirection.x, transform.position.y,
                        screenSpaceDirection.y);

                    Move(worldSpaceDirection);
                }
            }

            if (!PlayerInputHandler.IsMouseButtonUpThisFrame()) return;
            
            _startFingerTouchedPosition = Vector2.zero;
            _endFingerTouchedPosition = Vector2.zero;
        }

        private void Move(Vector3 direction)
        {
            transform.position += direction * (Time.deltaTime * _moveSpeed);
        }
    }
}
