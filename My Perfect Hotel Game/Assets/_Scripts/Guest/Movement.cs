using UnityEngine;

namespace Guest
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1.5f;

        public void MoveTo(Vector3 direction)
        {
            transform.position += direction * (Time.deltaTime * _moveSpeed);
        }
    }
}