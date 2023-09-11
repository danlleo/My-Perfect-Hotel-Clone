using UnityEngine;

namespace Player
{
    [SelectionBase]
    [RequireComponent(typeof(Interact))]
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _raycastStartPoint;
        
        private Interact _interact;

        private void Awake()
        {
            _interact = GetComponent<Interact>();
        }

        private void Update()
        {
            var ray = new Ray(_raycastStartPoint.position, _raycastStartPoint.forward);
            _interact.Check(ray);
        }
    }
}
