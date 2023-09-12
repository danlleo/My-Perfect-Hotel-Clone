using UnityEngine;

namespace Guest
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 4f;

        public void HandleMovement(Vector3 direction)
        {
            transform.position += direction * Time.deltaTime * _moveSpeed;
        }
    }
}